using System.Text.Json.Serialization;

namespace BikeUsers.Application.BikeUsages.Models;

/// <summary>
/// Represents the bike usage request.
/// </summary>
public class BikeUsageRequest
{
    [JsonPropertyName("Column2")]
    public int Id { get; set; } = 0;
    
    /// <summary>
    /// Requested date
    /// </summary>
    [JsonPropertyName("dteday")]
    public DateTimeOffset Date { get; set; }
    
    /// <summary>
    /// Recorded rental hours requested date
    /// </summary>
    [JsonPropertyName("hr")]
    public int Hour { get; set; }
    
    /// <summary>
    /// Requested date weather
    /// </summary>
    [JsonPropertyName("weathersit")]
    public WeatherSituation WeatherSituation { get; set; }
    
    /// <summary>
    /// Requested date temperature
    /// </summary>
    [JsonPropertyName("temp")]
    public double Temperature { get; set; }
    
    /// <summary>
    /// Requested date feel temperature
    /// </summary>
    [JsonPropertyName("atemp")]
    public double FeelTemperature { get; set; }
    
    /// <summary>
    /// Requested date humidity
    /// </summary>
    [JsonPropertyName("hum")]
    public double Humidity { get; set; }
    
    /// <summary>
    /// Requested date wind speed
    /// </summary>
    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }
    
    /// <summary>
    /// Requested date casual users
    /// </summary>
    [JsonPropertyName("casual")]
    public int Casual { get; set; }
}