using AutoMapper;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;
using FeedbackAnalyzer.Domain.Models.Entities;

namespace FeedbackAnalyzer.Infrastructure.Ratings.Mappers;

public class RatingMapper : Profile
{
    public RatingMapper()
    {
        CreateMap<TextAnalysisResult, Rating>()
            .ForMember(dest => dest.RedactedComment, opt => opt.MapFrom(src => src.RedactedText))
            .ForMember(
                dest => dest.CalculatingRating,
                opt => opt.ConvertUsing<SentimentConfidenceToRatingConverter, TextAnalysisSentimentConfidence>(src => src.SentimentConfidence)
            );
    }
}