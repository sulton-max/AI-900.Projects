using System.Text.Json.Serialization;

namespace BikeUsers.Application.BikeUsages.Models;

/// <summary>
/// Represents the bike usage response.
/// </summary>
public class BikeUsageResponse
{
    /// <summary>
    /// Vector of predicted bike users.
    /// </summary>
    [JsonPropertyName("Results")]
    public double[] Results { get; set; }
}