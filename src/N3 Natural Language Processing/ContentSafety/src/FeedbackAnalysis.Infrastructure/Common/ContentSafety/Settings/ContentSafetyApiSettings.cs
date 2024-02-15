using FeedbackAnalysis.Infrastructure.Common.Settings;

namespace FeedbackAnalysis.Infrastructure.Common.ContentSafety.Settings;

/// <summary>
/// Represents API settings for content safety.
/// </summary>
public class ContentSafetyApiSettings : ApiSettings
{
    /// <summary>
    /// Gets text analyze URL
    /// </summary>
    public string TextAnalyzeUrl { get; init; } = default!;

    /// <summary>
    /// Gets image analyze URL
    /// </summary>
    public string ImageAnalyzeUrl { get; init; } = default!;

    /// <summary>
    /// Gets image analyze URL
    /// </summary>
    public string ApiVersion { get; init; } = default!;

    /// <summary>
    /// Gets API key
    /// </summary>
    public string ApiKey { get; init; } = default!;
    
    /// <summary>
    /// Gets API key header
    /// </summary>
    public string ApiKeyHeader { get; init; } = default!;
}