namespace FeedbackAnalysis.Application.Common.ContentSafety.Models;

/// <summary>
/// Class representing a detailed detection result for a specific category.
/// </summary>
public class ContentSafetyCategoriesAnalysis
{
    /// <summary>
    /// The category of the detection result.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// The severity of the detection result.
    /// </summary>
    public int? Severity { get; set; }
}