using AutoMapper;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Brokers;
using FeedbackAnalyzer.Application.Ratings.Commands;
using FeedbackAnalyzer.Application.Ratings.Services;
using FeedbackAnalyzer.Domain.Enums;
using FeedbackAnalyzer.Domain.Models.Entities;

namespace FeedbackAnalyzer.Infrastructure.Ratings.Services;

/// <summary>
/// Provides rating processing service functionalities
/// </summary>
public class RatingProcessingService(IMapper mapper, ITextAnalyzerApiBroker textAnalyzerApiBroker) : IRatingProcessingService
{
    public async ValueTask<Rating> CreateRatingAsync(CreateRatingCommand createRatingCommand)
    {
        // Get rating analysis results 
        var ratingAnalysisResult = await textAnalyzerApiBroker.AnalyzeTextAsync(createRatingCommand.Comment);

        // Create rating
        var rating = mapper.Map<Rating>(ratingAnalysisResult);
        rating.Comment = createRatingCommand.Comment;
        rating.UserName = createRatingCommand.UserName;

        // Adjust sentiment
        if (rating.RatingSentiment == Sentiment.Neutral)
        {
            rating.RatingSentiment = rating.CalculatingRating switch
            {
                < 1.8 => Sentiment.Negative,
                > 4.2 => Sentiment.Positive,
                _ => rating.RatingSentiment
            };
        }

        return rating;
    }
}