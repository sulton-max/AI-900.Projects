using FaqApp.Application.KnowledgeBase.Brokers;
using FaqApp.Application.KnowledgeBase.Models;
using FaqApp.Application.KnowledgeBase.Services;

namespace FaqApp.Infrastructure.KnowledgeBase.Services;

/// <summary>
/// Provides knowledge base service functionality.
/// </summary>
public class KnowledgeBaseService(IKnowledgeBaseApiBroker knowledgeBaseApiBroker) : IKnowledgeBaseService
{
    public async ValueTask<KnowledgeBaseAnswer> GetAnswerAsync(KnowledgeBaseQuestion question)
    {
        // Validate question
        if (string.IsNullOrWhiteSpace(question.Question))
            throw new ArgumentNullException(nameof(question), "Question can't be empty");

        // Create request
        var request = new KnowledgeBaseRequest
        {
            Top = 3,
            Question = question.Question,
            IncludeUnstructuredSources = true,
            ConfidenceScoreThreshold = 0.7F,
            AnswerSpanRequest = new AnswerSpanRequest
            {
                Enable = true,
                TopAnswersWithSpan = 1,
                ConfidenceScoreThreshold = 0.7F
            }
        };
        
        // Get answer
        var response = await knowledgeBaseApiBroker.CheckAnswerAsync(request);
        return response?.Answers?.Select(
                answer => new KnowledgeBaseAnswer
                {
                    Answer = answer.AnswerText,
                    Source = answer.Source,
                    IsFound = answer.AnswerText != "No answer found"
                }
            )
            .FirstOrDefault() ?? new KnowledgeBaseAnswer
        {
            Answer = "Unfortunately, no answer found. Please try again later.",
        };
    }
}