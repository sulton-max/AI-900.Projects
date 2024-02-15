using FeedbackAnalysis.Domain.Enums;

namespace FeedbackAnalysis.Domain.Models.Confirmations;

/// <summary>
/// Represents  the decision made by the content moderation system.
/// </summary>
/// <param name="suggestedConfirmationAction">Suggested confirmation action</param>
/// <param name="actionByCategory">Actions grouped by category</param>
public class Decision(ConfirmationAction suggestedConfirmationAction, Dictionary<ContentSafetyCategory, ConfirmationAction> actionByCategory)
{
    /// <summary>
    /// Gets the suggested confirmation action.
    /// </summary>
    public ConfirmationAction SuggestedConfirmationAction { get; init; } = suggestedConfirmationAction;
    
    /// <summary>
    /// Gets the actions grouped by category.
    /// </summary>
    public Dictionary<ContentSafetyCategory, ConfirmationAction> ActionByCategory { get; init; } = actionByCategory;
}