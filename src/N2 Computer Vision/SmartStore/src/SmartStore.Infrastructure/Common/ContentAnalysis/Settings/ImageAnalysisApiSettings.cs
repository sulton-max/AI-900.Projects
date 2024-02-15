using SmartStore.Infrastructure.Common.Settings;

namespace SmartStore.Infrastructure.Common.ContentAnalysis.Settings;

/// <summary>
/// Represents image analysis API settings
/// </summary>
public class ImageAnalysisApiSettings : ApiSettings
{
    /// <summary>
    /// Gets API key
    /// </summary>
    public string ApiKey { get; init; } = default!;
}