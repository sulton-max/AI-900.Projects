using ResumeScanner.Infrastructure.Common.Settings;

namespace ResumeScanner.Infrastructure.Common.DocumentProcessing.Settings;

/// <summary>
/// Represents Azure Cognitive Service API settings
/// </summary>
public class AzureCognitiveServiceApiSettings : ApiSettings
{
    /// <summary>
    /// Gets api key
    /// </summary>
    public string ApiKey { get; init; } = default!;
}