using System.Text.Json.Serialization;

namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents knowledge base answer response
/// </summary>
public class KnowledgeBaseAnswerResponse
{
    /// <summary>
    /// Gets answers collection
    /// </summary>
    [JsonPropertyName("answers")] public ICollection<Answer>? Answers { get; init; }
}