using ProfileUpdater.Infrastructure.Common.Settings.ApiSettings;

namespace ProfileUpdater.Infrastructure.Common.ImageFaceAnalysis.Settings;

/// <summary>
/// Represents face recognition API settings
/// </summary>
public class FaceRecognitionApiSettings : ApiSettings
{
    /// <summary>
    /// Gets API key
    /// </summary>
    public string ApiKey { get; init; } = default!;

    /// <summary>
    /// Gets recognition model version
    /// </summary>
    public string RecognitionModel { get; init; } = default!;

    /// <summary>
    /// Gets detection model version
    /// </summary>
    public string DetectionModel { get; init; } = default!;
}