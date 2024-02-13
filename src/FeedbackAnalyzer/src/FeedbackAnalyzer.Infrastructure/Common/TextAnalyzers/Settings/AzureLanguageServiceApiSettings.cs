using FeedbackAnalyzer.Infrastructure.Common.Settings;

namespace FeedbackAnalyzer.Infrastructure.Common.TextAnalyzers.Settings;

/// <summary>
/// Represents Azure Cognitive Service API settings
/// </summary>
public class AzureLanguageServiceApiSettings : ApiSettings
{
    /// <summary>
    /// Gets api key
    /// </summary>
    public string ApiKey { get; init; } = default!;
}