namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Represents base class for detection result.
/// </summary>
public class ContentSafetyDetectionResult
{
    /// <summary>
    /// Gets the detailed result for categories analysis.
    /// </summary>
    public List<ContentSafetyCategoriesAnalysis>? CategoriesAnalysis { get; init; }
}