namespace FeedbackAnalysis.Domain.Models.Entities;

/// <summary>
/// Represents feedback from a customer.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Gets or sets the unique identifier for the feedback.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the comment from the customer.
    /// </summary>
    public string Comment { get; set; } = string.Empty;
}