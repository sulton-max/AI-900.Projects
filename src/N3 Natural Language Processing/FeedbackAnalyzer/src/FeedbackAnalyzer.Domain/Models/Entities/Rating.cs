using FeedbackAnalyzer.Domain.Enums;

namespace FeedbackAnalyzer.Domain.Models.Entities;

/// <summary>
/// Represents user rating
/// </summary>
public class Rating
{
    /// <summary>
    /// Gets or sets username
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Gets or sets user original comment
    /// </summary>
    public string Comment { get; set; } = default!;

    /// <summary>
    /// Gets or sets comment language
    /// </summary>
    public string Language { get; set; } = default!;

    /// <summary>
    /// Gets or sets comment key phrases
    /// </summary>
    public string[] KeyPhrases { get; set; } = default!;

    /// <summary>
    /// Gets or sets redacted comment without personal information
    /// </summary>
    public string RedactedComment { get; set; } = default!;

    /// <summary>
    /// Gets or sets calculated rating
    /// </summary>
    public double CalculatingRating { get; set; }

    /// <summary>
    /// Gets or sets rating sentiment
    /// </summary>
    public Sentiment RatingSentiment { get; set; }
}