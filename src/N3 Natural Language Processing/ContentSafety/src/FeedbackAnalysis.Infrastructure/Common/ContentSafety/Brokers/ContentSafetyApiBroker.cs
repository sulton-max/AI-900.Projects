using System.Text;
using System.Text.Json;
using FeedbackAnalysis.Application.Common.ContentSafety.Brokers;
using FeedbackAnalysis.Application.Common.ContentSafety.Models;
using FeedbackAnalysis.Application.Common.Serializers;
using FeedbackAnalysis.Domain.Enums;
using FeedbackAnalysis.Domain.Exceptions.ContentSafety;
using FeedbackAnalysis.Domain.Models.Confirmations;
using FeedbackAnalysis.Infrastructure.Common.ContentSafety.Settings;
using Microsoft.Extensions.Options;

namespace FeedbackAnalysis.Infrastructure.Common.ContentSafety.Brokers;

/// <summary>
/// Provides the content safety API broker.
/// </summary>
public class ContentSafetyApiBroker(
    HttpClient httpClient,
    IOptions<ContentSafetyApiSettings> contentSafetyApiSettings,
    IOptions<ContentSafetyThresholdSettings> contentSafetyThresholdSettings,
    IJsonSerializationSettingsProvider jsonSerializationSettingsProvider
) : IContentSafetyApiBroker
{
    private readonly ContentSafetyApiSettings _contentSafetyApiSettings = contentSafetyApiSettings.Value;
    private readonly ContentSafetyThresholdSettings _contentSafetyThresholdSettings = contentSafetyThresholdSettings.Value;

    /// <summary>
    /// The valid threshold values.
    /// </summary>
    // public static readonly int[] VALID_THRESHOLD_VALUES = [-1, 0, 2, 4, 6];
    public async ValueTask<ContentSafetyDetectionResult> DetectAsync(MediaType mediaType, string content, string[] blockItemsList)
    {
        // Build request URL and body
        var url = BuildUrl(mediaType);
        var requestBody = BuildRequestBody(mediaType, content, blockItemsList);
        var payload = JsonSerializer.Serialize(requestBody, requestBody.GetType(), jsonSerializationSettingsProvider.Get());

        // Create request message
        var message = new HttpRequestMessage(HttpMethod.Post, url);
        message.Content = new StringContent(payload, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

        // Send request and get response
        var response = await httpClient.SendAsync(message);
        var responseText = await response.Content.ReadAsStringAsync();

        // Convert response to result or error
        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<ContentSafetyDetectionErrorResponse>(responseText, jsonSerializationSettingsProvider.Get());

            return error?.Error?.Code == null || error.Error.Message == null
                ? throw new DetectionException(response.StatusCode.ToString(), $"Error is null. Response text is {responseText}")
                : throw new DetectionException(error.Error.Code, error.Error.Message);
        }

        var result = DeserializeDetectionResult(responseText, mediaType);

        if (result == null)
            throw new DetectionException(response.StatusCode.ToString(), $"HttpResponse is null. Response text is {responseText}");

        return result;
    }

    public Decision MakeDecision(ContentSafetyDetectionResult contentSafetyDetectionResult)
    {
        // Calculate the confirmation action for each category
        var actionsByCategory = new Dictionary<ContentSafetyCategory, ConfirmationAction>(
            _contentSafetyThresholdSettings.CategoryThresholds.Select(
                threshold =>
                {
                    // Get detected severity
                    var severity =
                        contentSafetyDetectionResult.CategoriesAnalysis?.FirstOrDefault(result => result.Category == threshold.Key.ToString())
                            ?.Severity ?? throw new ArgumentException($"Can not find detection result for {threshold.Key}");

                    var confirmationAction = threshold.Value != ContentSafetyThreshold.Low && severity >= (int)threshold.Value
                        ? ConfirmationAction.Reject
                        : ConfirmationAction.Accept;

                    return new KeyValuePair<ContentSafetyCategory, ConfirmationAction>(threshold.Key, confirmationAction);
                }
            )
        );

        // Final confirmation action is rejected if any of the action result is rejected
        var finalConfirmationAction = actionsByCategory.Any(result => result.Value == ConfirmationAction.Reject)
            ? ConfirmationAction.Reject
            : ConfirmationAction.Accept;

        // Check matched block items
        if (contentSafetyDetectionResult is TextSafetyDetectionResult { BlockItemListMatch.Count: > 0 })
            finalConfirmationAction = ConfirmationAction.Reject;

        return new Decision(finalConfirmationAction, actionsByCategory);
    }

    // /// <summary>
    // /// Gets the severity score of the specified contentSafetyCategory from the given detection result.
    // /// </summary>
    // /// <param name="contentSafetyCategory">The contentSafetyCategory to get the severity score for.</param>
    // /// <param name="contentSafetyDetectionResult">The detection result object to retrieve the severity score from.</param>
    // /// <returns>The severity score of the specified contentSafetyCategory.</returns>
    // private int? GetDetectionResultByCategory(ContentSafetyCategory contentSafetyCategory, ContentSafetyDetectionResult contentSafetyDetectionResult)
    // {
    //     // int? severityResult = null;
    //     // if (contentSafetyDetectionResult.CategoriesAnalysis != null)
    //     // {
    //     //     foreach (var detailedResult in contentSafetyDetectionResult.CategoriesAnalysis)
    //     //     {
    //     //         if (detailedResult.Category == contentSafetyCategory.ToString())
    //     //         {
    //     //             severityResult = detailedResult.Severity;
    //     //         }
    //     //     }
    //     // }
    //
    //     return;
    //
    //     // return severityResult;
    // }

    /// <summary>
    /// Builds the URL for the Content Safety API based on the media type.
    /// </summary>
    /// <param name="mediaType">The type of media to analyze.</param>
    /// <returns>The URL for the Content Safety API.</returns>
    private string BuildUrl(MediaType mediaType)
    {
        return mediaType switch
        {
            MediaType.Text =>
                $"{_contentSafetyApiSettings.TextAnalyzeUrl}?{_contentSafetyApiSettings.ApiVersionHeader}={_contentSafetyApiSettings.ApiVersion}",
            MediaType.Image =>
                $"{_contentSafetyApiSettings.ImageAnalyzeUrl}?{_contentSafetyApiSettings.ApiVersionHeader}={_contentSafetyApiSettings.ApiVersion}",
            _ => throw new ArgumentException($"Invalid Media Type {mediaType}")
        };
    }

    /// <summary>
    /// Builds the request body for the Content Safety API request.
    /// </summary>
    /// <param name="mediaType">The type of media to analyze.</param>
    /// <param name="content">The content to analyze.</param>
    /// <param name="blockItemsList">The block lists to use for text analysis.</param>
    /// <returns>The request body for the Content Safety API request.</returns>
    private ContentSafetyDetectionRequest BuildRequestBody(MediaType mediaType, string content, string[] blockItemsList)
    {
        return mediaType switch
        {
            MediaType.Text => new TextSafetyDetectionRequest(content, blockItemsList),
            MediaType.Image => new ImageSafetyDetectionRequest(content),
            _ => throw new ArgumentException($"Invalid Media Type {mediaType}")
        };
    }

    /// <summary>
    /// Deserializes the JSON string into a ContentSafetyDetectionResult object based on the media type.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="mediaType">The media type of the detection result.</param>
    /// <returns>The deserialized ContentSafetyDetectionResult object for the Content Safety API response.</returns>
    private ContentSafetyDetectionResult? DeserializeDetectionResult(string json, MediaType mediaType)
    {
        return mediaType switch
        {
            MediaType.Text => JsonSerializer.Deserialize<TextSafetyDetectionResult>(json, jsonSerializationSettingsProvider.Get()),
            MediaType.Image => JsonSerializer.Deserialize<ImageContentSafetyDetectionResult>(json, jsonSerializationSettingsProvider.Get()),
            _ => throw new ArgumentException($"Invalid Media Type {mediaType}")
        };
    }
}