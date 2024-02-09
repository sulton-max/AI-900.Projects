namespace FeedbackAnalysis.Infrastructure.Common.Settings;

/// <summary>
/// Represents API settings
/// </summary>
public class ApiSettings
{
    /// <summary>
    /// Gets base address of API
    /// </summary>
    public string BaseAddress { get; init; } = string.Empty;

    /// <summary>
    /// Gets API version header
    /// </summary>
    public string ApiVersionHeader { get; init; } = string.Empty;
}