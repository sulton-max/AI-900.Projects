using System.Text.Json;

namespace FeedbackAnalysis.Application.Common.Serializers;

/// <summary>
/// Interface for providing JSON serialization settings.
/// </summary>
public interface IJsonSerializationSettingsProvider
{
    /// <summary>
    /// Gets JSON serialization settings.
    /// </summary>
    /// <returns>JsonSerializerSettings for customizing JSON serialization.</returns>
    JsonSerializerOptions Get();
}