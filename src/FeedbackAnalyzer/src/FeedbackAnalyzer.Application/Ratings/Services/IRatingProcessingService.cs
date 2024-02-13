using FeedbackAnalyzer.Application.Ratings.Commands;
using FeedbackAnalyzer.Domain.Models.Entities;

namespace FeedbackAnalyzer.Application.Ratings.Services;

/// <summary>
/// Defines rating processing service functionalities
/// </summary>
public interface IRatingProcessingService
{
    /// <summary>
    /// Analyzes and creates rating
    /// </summary>
    /// <param name="createRatingCommand">Create rating command - this is CQRS command in real world scenario</param>
    /// <returns>Created rating</returns>
    ValueTask<Rating> CreateRatingAsync(CreateRatingCommand createRatingCommand);
}