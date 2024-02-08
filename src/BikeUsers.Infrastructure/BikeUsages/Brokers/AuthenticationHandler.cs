using System.Net.Http.Headers;
using BikeUsers.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace BikeUsers.Infrastructure.BikeUsages.Brokers;

public class AuthenticationHandler(IOptions<BikeUsagePredictionApiSettings> bikeUsageApiPredictionSettings) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bikeUsageApiPredictionSettings.Value.ApiKey);

        return base.SendAsync(request, cancellationToken);
    }
}