using System.Text.Json.Serialization;

namespace BikeUsers.Application.BikeUsages.Models;

/// <summary>
/// Represents Machine Learning model request
/// </summary>
public class ModelRequest(object data)
{
    /// <summary>
    /// Gets or sets the inputs
    /// </summary>
    [JsonPropertyName("Inputs")]
    public dynamic Inputs { get; set; } = new
    {
        data = new[] { data }
    };

    /// <summary>
    /// Gets or sets the global parameters
    /// </summary>
    [JsonPropertyName("GlobalParameters")]
    public double GlobalParameters { get; set; }
}