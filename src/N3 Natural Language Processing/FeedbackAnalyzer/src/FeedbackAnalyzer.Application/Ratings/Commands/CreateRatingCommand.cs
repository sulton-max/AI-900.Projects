namespace FeedbackAnalyzer.Application.Ratings.Commands;

/// <summary>
/// Represents rating creation command
/// </summary>
public record CreateRatingCommand
{
    /// <summary>
    /// Gets username of user creating the feedback
    /// </summary>
    public string UserName { get; init; } = default!;

    /// <summary>
    /// Gets rating comment
    /// </summary>
    public string Comment { get; init; } = default!;
}