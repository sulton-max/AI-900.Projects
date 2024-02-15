using Azure.AI.TextAnalytics;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Brokers;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;
using FeedbackAnalyzer.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackAnalyzer.Infrastructure.Common.TextAnalyzers.Brokers;

/// <summary>
/// Provides the text analyzer API broker
/// </summary>
public class TextAnalyzerApiBroker([FromKeyedServices("RatingAnalysis")] TextAnalyticsClient textAnalyticsClient) : ITextAnalyzerApiBroker
{
    public async ValueTask<TextAnalysisResult> AnalyzeTextAsync(string text)
    {
        // Detect language
        var language = await textAnalyticsClient.DetectLanguageAsync(text);

        // Detect key phrases
        var keyPhrases = await textAnalyticsClient.ExtractKeyPhrasesAsync(text);

        // Recognize personal information
        var redactedText = await textAnalyticsClient.RecognizePiiEntitiesAsync(text);

        // Mine opinion
        var opinion = await textAnalyticsClient.AnalyzeSentimentAsync(text);

        return new TextAnalysisResult
        {
            Language = language.Value.Name,
            KeyPhrases = keyPhrases.Value.ToArray(),
            RedactedText = redactedText.Value.RedactedText,
            RatingSentiment = opinion.Value.Sentiment switch
            {
                TextSentiment.Negative => Sentiment.Negative,
                TextSentiment.Neutral or TextSentiment.Mixed => Sentiment.Neutral,
                TextSentiment.Positive => Sentiment.Positive,
                _ => throw new ArgumentOutOfRangeException()
            },
            SentimentConfidence = new TextAnalysisSentimentConfidence
            {
                NegativeConfidenceScore = opinion.Value.ConfidenceScores.Negative,
                NeutralConfidenceScore = opinion.Value.ConfidenceScores.Neutral,
                PositiveConfidenceScore = opinion.Value.ConfidenceScores.Positive
            }
        };
    }
}