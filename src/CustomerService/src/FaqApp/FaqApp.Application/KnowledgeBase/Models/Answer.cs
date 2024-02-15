using System.Text.Json.Serialization;

namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents knowledge base request answer
/// </summary>
public record Answer
{
    /// <summary>
    /// Gets the questions
    /// </summary>
    [JsonPropertyName("questions")]
    public List<string> Questions { get; init; } = default!;

    /// <summary>
    /// Gets the answers
    /// </summary>
    [JsonPropertyName("answer")]
    public string AnswerText { get; init; } = default!;

    /// <summary>
    ///  Gets the confidence score
    /// </summary>
    [JsonPropertyName("confidenceScore")]
    public float ConfidenceScore { get; init; }

    /// <summary>
    /// Gets the id
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// Gets the answer source
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; init; } = default!;
}