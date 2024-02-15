using FeedbackAnalyzer.Domain.Enums;

namespace FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;

/// <summary>
/// Represents text analysis result
/// </summary>
public class TextAnalysisResult
{
    /// <summary>
    /// Gets text language
    /// </summary>
    public string Language { get; init; } = default!;

    /// <summary>
    /// Gets text key phrases
    /// </summary>
    public string[] KeyPhrases { get; init; } = default!;

    /// <summary>
    /// Gets redacted text without personal information
    /// </summary>
    public string RedactedText { get; init; } = default!;

    /// <summary>
    /// Gets rating sentiment
    /// </summary>
    public Sentiment RatingSentiment { get; init; }

    /// <summary>
    /// Gets sentiment confidence
    /// </summary>
    public TextAnalysisSentimentConfidence SentimentConfidence { get; init; } = default!;
}