using System.Text.Json;
using System.Text.Json.Serialization;
using FeedbackAnalysis.Application.Common.Serializers;

namespace FeedbackAnalysis.Infrastructure.Common.Serializers;

/// <summary>
/// Interface for providing JSON serialization settings.
/// </summary>
public class JsonSerializationSettingsProvider : IJsonSerializationSettingsProvider
{
    public JsonSerializerOptions Get()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }
}