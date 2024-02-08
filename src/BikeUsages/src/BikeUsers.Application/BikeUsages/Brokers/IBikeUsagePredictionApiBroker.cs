using BikeUsers.Application.BikeUsages.Models;

namespace BikeUsers.Application.BikeUsages.Brokers;

/// <summary>
/// Defines the bike usage prediction model endpoint client.
/// </summary>
public interface IBikeUsagePredictionApiBroker
{
    /// <summary>
    /// Gets the predicted bike users.
    /// </summary>
    /// <param name="request">Bike usage request</param>
    /// <returns>Bike usage prediction</returns>
    ValueTask<BikeUsageResponse?> GetPredictedUsersAsync(BikeUsageRequest request);
}