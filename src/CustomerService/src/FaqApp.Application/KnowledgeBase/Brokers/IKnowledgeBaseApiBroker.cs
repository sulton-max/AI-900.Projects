using FaqApp.Application.KnowledgeBase.Models;

namespace FaqApp.Application.KnowledgeBase.Brokers;

/// <summary>
/// Defines knowledge base API broker functionality.
/// </summary>
public interface IKnowledgeBaseApiBroker
{
    /// <summary>
    /// Checks knowledge base answer from sources
    /// </summary>
    /// <param name="request">User request with request options</param>
    /// <returns>Knowledge base response if answers if any found</returns>
    ValueTask<KnowledgeBaseAnswerResponse> CheckAnswerAsync(KnowledgeBaseRequest request);    
}