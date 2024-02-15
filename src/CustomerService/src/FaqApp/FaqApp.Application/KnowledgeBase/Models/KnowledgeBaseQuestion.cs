namespace FaqApp.Application.KnowledgeBase.Models;

/// <summary>
/// Represents knowledge base question
/// </summary>
public sealed record KnowledgeBaseQuestion
{
    /// <summary>
    /// Gets question content
    /// </summary>
    public string Question { get; init; } = default!;
}