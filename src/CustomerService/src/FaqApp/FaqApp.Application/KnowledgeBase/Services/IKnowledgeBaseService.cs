using FaqApp.Application.KnowledgeBase.Models;

namespace FaqApp.Application.KnowledgeBase.Services;

/// <summary>
/// Defines knowledge base service functionality.
/// </summary>
public interface IKnowledgeBaseService
{
    /// <summary>
    /// Sends a question to the knowledge base and returns an answer
    /// </summary>
    /// <param name="question">User question</param>
    /// <returns>Knowledge base answer if any found</returns>
    ValueTask<KnowledgeBaseAnswer> GetAnswerAsync(KnowledgeBaseQuestion question);
}