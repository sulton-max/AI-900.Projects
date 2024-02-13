using AutoMapper;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;

namespace FeedbackAnalyzer.Infrastructure.Ratings.Mappers;

public class SentimentConfidenceToRatingConverter : IValueConverter<TextAnalysisSentimentConfidence, double>
{
    public double Convert(TextAnalysisSentimentConfidence sourceMember, ResolutionContext context)
    {
        // This would be retrieved from external configuration in a real-world scenario 
        var negativeWeight = 0.83; // Midpoint of negative range
        var neutralWeight = 2.5; // Midpoint of neutral range
        var positiveWeight = 4.17; // Midpoint of positive range

        return sourceMember.NegativeConfidenceScore * negativeWeight 
               + sourceMember.NeutralConfidenceScore * neutralWeight 
               + sourceMember.PositiveConfidenceScore * positiveWeight;
    }
}