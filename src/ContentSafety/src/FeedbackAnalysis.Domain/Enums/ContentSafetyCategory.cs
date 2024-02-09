namespace FeedbackAnalysis.Domain.Enums;

/// <summary>
/// Represents the content safety category.
/// </summary>
public enum ContentSafetyCategory
{
    /// <summary>
    /// Refers to hate speech.
    /// </summary>
    Hate = 0,
    
    /// <summary>
    /// Refers to self harm.
    /// </summary>
    SelfHarm = 1,
    
    /// <summary>
    /// Refers to sexual content.
    /// </summary>
    Sexual = 2,
    
    /// <summary>
    /// Refers to violence.
    /// </summary>
    Violence = 3
}