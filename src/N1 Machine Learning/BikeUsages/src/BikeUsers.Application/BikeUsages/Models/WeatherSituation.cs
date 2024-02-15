namespace BikeUsers.Application.BikeUsages.Models;

/// <summary>
/// Represents the weather situation.
/// </summary>
public enum WeatherSituation
{
    /// <summary>
    /// Indicates few clouds, partly cloudy
    /// </summary>
    Clear = 1,
    
    /// <summary>
    /// Indicates cloudy, mist + broken clouds, mist + few clouds, mist
    /// </summary>
    Mist = 2,
    
    /// <summary>
    /// Indicates light rain, light snow, light rain + thunderstorm + scattered clouds, light rain + scattered clouds
    /// </summary>
    LightRain = 3,
    
    /// <summary>
    /// Indicates heavy rain + ice pallets + thunderstorm + mist, snow + fog
    /// </summary>
    HeavyRain = 4
}