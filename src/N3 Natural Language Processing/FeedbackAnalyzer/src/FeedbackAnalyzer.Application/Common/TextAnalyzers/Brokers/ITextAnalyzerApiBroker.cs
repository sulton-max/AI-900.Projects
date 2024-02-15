using FeedbackAnalyzer.Application.Common.TextAnalyzers.Models;

namespace FeedbackAnalyzer.Application.Common.TextAnalyzers.Brokers;

/// <summary>
/// Defines the text analyzer API broker
/// </summary>
public interface ITextAnalyzerApiBroker
{
    /// <summary>
    /// Analyzes text
    /// </summary>
    /// <param name="text">Text to analyze</param>
    /// <returns>Text analysis result</returns>
    ValueTask<TextAnalysisResult> AnalyzeTextAsync(string text);
}