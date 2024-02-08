using System.Net.Http.Json;
using BikeUsers.Application.BikeUsages.Brokers;
using BikeUsers.Application.BikeUsages.Models;

namespace BikeUsers.Infrastructure.BikeUsages.Brokers;

/// <summary>
/// Provides the bike usage prediction API client functionality.
/// </summary>
public class BikeUsagePredictionApiClient(HttpClient httpClient) : IBikeUsagePredictionApiClient
{
    public async ValueTask<BikeUsageResponse?> GetPredictedUsersAsync(BikeUsageRequest request)
    {
        var response = await httpClient.PostAsJsonAsync<ModelRequest>("", new ModelRequest(request));
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<BikeUsageResponse>() : null;
    }
}