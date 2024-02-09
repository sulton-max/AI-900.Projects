using FeedbackAnalysis.Application.Common.ContentSafety.Brokers;
using FeedbackAnalysis.Application.Feedbacks.Services;
using FeedbackAnalysis.Domain.Enums;
using FeedbackAnalysis.Domain.Models.Confirmations;
using FeedbackAnalysis.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace FeedbackAnalysis.Infrastructure.Feedbacks.Services;

/// <summary>
/// Provides feedback processing service functionalities.
/// </summary>
public class FeedbackProcessingService(ILogger<FeedbackProcessingService> logger, IContentSafetyApiBroker contentSafetyApiBroker)
    : IFeedbackProcessingService
{
    public async ValueTask<Feedback> CreateAsync(Feedback feedback)
    {
        // Validate for content safety
        var contentSafetyResult = await contentSafetyApiBroker.DetectAsync(MediaType.Text, feedback.Comment, []);
        var decision = contentSafetyApiBroker.MakeDecision(contentSafetyResult);

        // Set confirmation action based on decision
        feedback.ConfirmationAction = decision.SuggestedConfirmationAction;

        if (feedback.ConfirmationAction == ConfirmationAction.Reject)
            logger.LogWarning("Feedback was rejected due to unsafe content.");

        // Storing feedback in database ...

        return feedback;
    }
}