using System.Text.Json.Serialization;

namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents knowledge base request
/// </summary>
public class KnowledgeBaseRequest
{
    /// <summary>
    /// Gets or sets top answers count
    /// </summary>
    [JsonPropertyName("top")] 
    public int Top { get; set; }

    /// <summary>
    /// Gets or sets the question
    /// </summary>
    [JsonPropertyName("question")] 
    public string Question { get; set; } = default!;

    /// <summary>
    /// Gets or sets flag to include unstructured sources
    /// </summary>
    [JsonPropertyName("includeUnstructuredSources")]
    public bool IncludeUnstructuredSources { get; set; }

    /// <summary>
    /// Gets or sets confidence threshold
    /// </summary>
    [JsonPropertyName("confidenceScoreThreshold")]
    public float ConfidenceScoreThreshold { get; set; } = 0.6F;

    /// <summary>
    /// Gets or sets answer span request
    /// </summary>
    [JsonPropertyName("answerSpanRequest")]
    public AnswerSpanRequest AnswerSpanRequest { get; set; } = default!;
}