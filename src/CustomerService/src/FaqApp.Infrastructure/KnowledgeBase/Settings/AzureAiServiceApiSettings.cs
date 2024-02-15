using System.Text.Json.Serialization;
using FaqApp.Infrastructure.Common.Settings;

namespace FaqApp.Infrastructure.KnowledgeBase.Settings;

/// <summary>
/// Represents Azure AI Service API settings
/// </summary>
public sealed record AzureAiServiceApiSettings : ApiSettings
{
    /// <summary>
    /// Gets the project name query parameter name
    /// </summary>
    [JsonIgnore]
    public string ProjectNameQueryParameterName { get; init; } = "projectName";

    /// <summary>
    /// Gets the API version query parameters name
    /// </summary>
    [JsonIgnore]
    public string ApiVersionQueryParametersName { get; init; } = "api-version";

    /// <summary>
    /// Gets the deployment name query parameter name
    /// </summary>
    [JsonIgnore]
    public string DeploymentNameQueryParameterName { get; init; } = "deploymentName";
    
    /// <summary>
    /// Gets the API key header name
    /// </summary>
    [JsonIgnore]
    public string ApiKeyHeaderName { get; init; } = "Ocp-Apim-Subscription-Key";

    /// <summary>
    /// Gets project name
    /// </summary>
    public string ProjectName { get; init; } = default!;

    /// <summary>
    /// Gets the API version
    /// </summary>
    public string ApiVersion { get; init; } = default!;

    /// <summary>
    /// Gets the deployment name
    /// </summary>
    public string DeploymentName { get; init; } = default!;
    
    /// <summary>
    /// Gets the API key
    /// </summary>
    public string ApiKey { get; init; } = default!;
}