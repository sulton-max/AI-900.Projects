using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Web;
using FaqApp.Application.KnowledgeBase.Brokers;
using FaqApp.Application.KnowledgeBase.Models;
using FaqApp.Infrastructure.KnowledgeBase.Settings;
using Microsoft.Extensions.Options;

namespace FaqApp.Infrastructure.KnowledgeBase.Brokers;

/// <summary>
/// Provides knowledge base API broker functionality.
/// </summary>
public class KnowledgeBaseApiBroker(
    HttpClient httpClient,
    IOptions<AzureServiceSettings> azureServiceSettings,
    IOptions<AzureAiServiceApiSettings> azureAiServiceApiSettings
) : IKnowledgeBaseApiBroker
{
    private readonly AzureServiceSettings _azureServiceSettings = azureServiceSettings.Value;
    private readonly AzureAiServiceApiSettings _azureAiServiceApiSettings = azureAiServiceApiSettings.Value;

    public async ValueTask<KnowledgeBaseAnswerResponse> CheckAnswerAsync(KnowledgeBaseRequest request)
    {
        // Build request URL
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters[_azureAiServiceApiSettings.ProjectNameQueryParameterName] = _azureAiServiceApiSettings.ProjectName;
        queryParameters[_azureAiServiceApiSettings.ApiVersionQueryParametersName] = _azureAiServiceApiSettings.ApiVersion;
        queryParameters[_azureAiServiceApiSettings.DeploymentNameQueryParameterName] = _azureAiServiceApiSettings.DeploymentNameQueryParameterName;

        var uriBuilder = new UriBuilder(_azureAiServiceApiSettings.BaseAddress)
        {
            Query = queryParameters.ToString(),
            Path = _azureServiceSettings.KnowledgeBaseFeatureUrl
        };

        // uriBuilder.Path += _azureServiceSettings.KnowledgeBaseFeatureUrl;

        var requestUrl = uriBuilder.ToString();
        
        // var requestUrl = $"{_azureServiceSettings.KnowledgeBaseFeatureUrl}?{queryParameters}";

        // Send request
        var response = await httpClient.PostAsJsonAsync(requestUrl, request);
        response.EnsureSuccessStatusCode();

        // Parse response
        var knowledgeBaseAnswerResponse = await response.Content.ReadFromJsonAsync<KnowledgeBaseAnswerResponse>();
        return knowledgeBaseAnswerResponse!;
    }
}