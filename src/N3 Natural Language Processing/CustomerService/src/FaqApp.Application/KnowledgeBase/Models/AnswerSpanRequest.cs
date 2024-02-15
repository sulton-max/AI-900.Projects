using System.Text.Json.Serialization;

namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents answer span request
/// </summary>
public class AnswerSpanRequest
{
    /// <summary>
    /// Gets or sets flag to enable answer span
    /// </summary>
    [JsonPropertyName("enable")] 
    public bool Enable { get; set; }

    /// <summary>
    /// Gets or sets top answers with span
    /// </summary>
    [JsonPropertyName("topAnswersWithSpan")]
    public int TopAnswersWithSpan { get; set; }

    /// <summary>
    /// Gets or sets confidence threshold
    /// </summary>
    [JsonPropertyName("confidenceScoreThreshold")]
    public float ConfidenceScoreThreshold { get; set; } = 0.6F;
}