using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Domain.Exceptions.ContentSafety;

/// <summary>
/// Represents a content safety detection error.
/// </summary>
public class ContentSafetyDetectionError
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Gets the error target.
    /// </summary>
    [JsonPropertyName("target")]
    public string? Target { get; init; }

    /// <summary>
    /// Gets the error details.
    /// </summary>
    [JsonPropertyName("details")]
    public string[]? Details { get; init; }

    /// <summary>
    /// Gets the inner error.
    /// </summary>
    [JsonPropertyName("innererror")]
    public ContentSafetyDetectionInnerError? InnerError { get; init; }
}