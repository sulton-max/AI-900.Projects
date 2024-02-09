using FeedbackAnalysis.Domain.Enums;

namespace FeedbackAnalysis.Infrastructure.Common.ContentSafety.Settings;

/// <summary>
/// Represents the content safety threshold settings.
/// </summary>
public class ContentSafetyThresholdSettings
{
    /// <summary>
    /// Gets content safety category thresholds.
    /// </summary>
    public Dictionary<ContentSafetyCategory, ContentSafetyThreshold> CategoryThresholds { get; init; } = default!;
}