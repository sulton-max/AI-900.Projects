namespace BikeUsers.Infrastructure.Settings;

/// <summary>
/// Represents the bike usage prediction API settings
/// </summary>
public class BikeUsagePredictionApiSettings : ApiSettings
{
    /// <summary>
    /// Gets or sets the API key
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}