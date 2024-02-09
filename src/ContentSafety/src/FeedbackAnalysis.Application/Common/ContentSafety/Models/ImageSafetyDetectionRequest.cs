using System.Text.Json.Serialization;
using FeedbackAnalysis.Domain.Models.Media;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Class representing an image detection request.
/// </summary>
public class ImageSafetyDetectionRequest(string content) : ContentSafetyDetectionRequest
{
    /// <summary>
    /// Gets the image to be analyzed.
    /// </summary>
    [JsonPropertyName("Image")]
    public Base64Image Base64Image { get; init; } = new Base64Image(content);
}