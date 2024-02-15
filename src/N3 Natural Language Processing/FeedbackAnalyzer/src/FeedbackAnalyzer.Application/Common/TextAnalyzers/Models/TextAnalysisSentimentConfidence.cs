namespace FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;

/// <summary>
/// Represents text analysis sentiment confidence
/// </summary>
public class TextAnalysisSentimentConfidence
{
    /// <summary>
    /// Gets positive confidence score
    /// </summary>
    public double PositiveConfidenceScore { get; init; }

    /// <summary>
    /// Gets neutral confidence score
    /// </summary>
    public double NeutralConfidenceScore { get; init; }

    /// <summary>
    /// Gets negative confidence score
    /// </summary>
    public double NegativeConfidenceScore { get; init; }
}