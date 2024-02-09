using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Class representing a text detection request.
/// </summary>
public class TextSafetyDetectionRequest(string text, string[] blockListItemNames) : ContentSafetyDetectionRequest
{
    /// <summary>
    /// Gets the text to be detected.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = text;

    /// <summary>
    /// Gets the names of the block lists to use for detecting the text.
    /// </summary>
    [JsonPropertyName("blocklistNames")]
    public string[] BlockListItemNames { get; init; } = blockListItemNames;
}