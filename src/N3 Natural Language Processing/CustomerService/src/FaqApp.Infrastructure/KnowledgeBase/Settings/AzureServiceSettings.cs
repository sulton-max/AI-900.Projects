namespace FaqApp.Infrastructure.KnowledgeBase.Settings;

/// <summary>
/// Represents Azure Question Answering service API settings
/// </summary>
public sealed record AzureServiceSettings
{
    /// <summary>
    /// Gets knowledge base feature URL
    /// </summary>
    public string KnowledgeBaseFeatureUrl { get; init; } = default!;
}