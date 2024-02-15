using FeedbackAnalysis.Domain.Models.Entities;

namespace FeedbackAnalysis.Application.Feedbacks.Services;

/// <summary>
/// Defines feedback processing service functionalities.
/// </summary>
public interface IFeedbackProcessingService
{
    /// <summary>
    /// Creates and analyses a new feedback.
    /// </summary>
    /// <param name="feedback">Feedback to create</param>
    /// <returns>Created feedback</returns>
    ValueTask<Feedback> CreateAsync(Feedback feedback);
}