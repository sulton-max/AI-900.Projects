using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Class representing a text detection result.
/// </summary>
public class TextSafetyDetectionResult : ContentSafetyDetectionResult
{
    /// <summary>
    /// The list of detailed results for block list matches.
    /// </summary>
    [JsonPropertyName("blocklistsMatch")]
    public List<BlockListItemMatch>? BlockItemListMatch { get; set; }
}