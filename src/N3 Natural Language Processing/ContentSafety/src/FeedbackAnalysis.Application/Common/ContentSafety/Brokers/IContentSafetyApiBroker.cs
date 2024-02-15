using FeedbackAnalysis.Application.Common.ContentSafety.Models;
using FeedbackAnalysis.Domain.Enums;
using FeedbackAnalysis.Domain.Models.Confirmations;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Brokers;

/// <summary>
/// Defines the content safety API broker.
/// </summary>
public interface IContentSafetyApiBroker
{
    /// <summary>
    /// Detects unsafe content using the Content Safety API.
    /// </summary>
    /// <param name="mediaType">The media type of the content to detect.</param>
    /// <param name="content">The content to detect.</param>
    /// <param name="blockItemsList">The blockItemsList to use for text detection.</param>
    /// <returns>The response from the Content Safety API.</returns>
    ValueTask<ContentSafetyDetectionResult> DetectAsync(MediaType mediaType, string content, string[] blockItemsList);

    /// <summary>
    /// Makes a decision based on the detection result and the specified reject thresholds.
    /// Users can customize their decision-making method.
    /// </summary>
    /// <param name="contentSafetyDetectionResult">The detection result object to make the decision on.</param>
    /// <returns>The decision made based on the detection result and the specified reject thresholds.</returns>
    Decision MakeDecision(ContentSafetyDetectionResult contentSafetyDetectionResult);
}