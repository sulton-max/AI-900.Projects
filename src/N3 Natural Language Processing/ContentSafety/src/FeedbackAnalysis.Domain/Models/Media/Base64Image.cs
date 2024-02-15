using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Domain.Models.Media;

/// <summary>
/// Represents an image to be analyzed for content safety
/// </summary>
public class Base64Image(string content)
{
    /// <summary>
    /// Gets the base64-encoded content of the image.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = content;
}