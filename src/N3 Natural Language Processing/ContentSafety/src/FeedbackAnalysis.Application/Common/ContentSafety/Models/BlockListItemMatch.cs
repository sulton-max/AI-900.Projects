using System.Text.Json.Serialization;

namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Class representing a detailed detection result for a block list item match.
/// </summary>
public class BlockListItemMatch
{
    /// <summary>
    /// Gets the name of the block list item
    /// </summary>
    [JsonPropertyName("blocklistName")]
    public string? BlockListItemName { get; init; }

    /// <summary>
    /// Gets the ID of the block item that matched.
    /// </summary>
    [JsonPropertyName("blocklistItemId")]
    public string? BlockListItemId { get; init; }

    /// <summary>
    /// Gets the text of the block item that matched.
    /// </summary>
    [JsonPropertyName("blocklistItemText")]
    public string? BlockListItemText { get; init; }
}