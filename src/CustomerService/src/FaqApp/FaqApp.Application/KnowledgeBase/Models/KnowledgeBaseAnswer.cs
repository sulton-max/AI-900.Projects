namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents knowledge base answer
/// </summary>
public sealed record KnowledgeBaseAnswer
{
    /// <summary>
    /// Gets answer content
    /// </summary>
    public string Answer { get; init; } = default!;

    /// <summary>
    /// Gets answer source
    /// </summary>
    public string Source { get; init; } = default!;
    
    /// <summary>
    /// Gets a value indicating whether the answer is found
    /// </summary>
    public bool IsFound { get; init; } = default!;
}