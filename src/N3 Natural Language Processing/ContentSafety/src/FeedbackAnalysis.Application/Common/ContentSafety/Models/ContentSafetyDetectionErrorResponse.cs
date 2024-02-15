using System.Text.Json.Serialization;
using FeedbackAnalysis.Domain.Exceptions.ContentSafety;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Represents content safety detection error response
/// </summary>
public class ContentSafetyDetectionErrorResponse
{
    /// <summary>
    /// Gets content safety detection error.
    /// </summary>
    [JsonPropertyName("error")]
    public ContentSafetyDetectionError? Error { get; init; }
}