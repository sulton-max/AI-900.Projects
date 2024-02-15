namespace FeedbackAnalysis.Domain.Enums;

/// <summary>
/// Represents enumeration for content safety threshold.
/// </summary>
public enum ContentSafetyThreshold
{
    /// <summary>
    /// Indicates low threshold, any unsafe content will be rejected
    /// </summary>
    Low = -1,

    /// <summary>
    /// Indicates neutral threshold, some bearable hate and violent content may be accepted
    /// </summary>
    Neutral = 0,

    /// <summary>
    /// Indicates medium threshold, some medium hate and violent content may be accepted
    /// </summary>
    Medium = 2,

    /// <summary>
    /// Indicates high threshold, some unsafe content may be accepted
    /// </summary>
    High = 4,

    /// <summary>
    /// Indicates very high threshold, any unsafe content will be accepted
    /// </summary>
    VeryHigh = 6
}