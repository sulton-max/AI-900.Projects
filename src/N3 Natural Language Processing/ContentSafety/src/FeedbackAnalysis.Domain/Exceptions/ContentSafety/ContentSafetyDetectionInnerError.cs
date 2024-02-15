using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Domain.Exceptions.ContentSafety;

/// <summary>
/// Represents the content safety inner error.
/// </summary>
public class ContentSafetyDetectionInnerError
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }
    
    /// <summary>
    /// Gets the inner error
    /// </summary>
    [JsonPropertyName("innererror")]
    public ContentSafetyDetectionInnerError? InnerError { get; init; }
}