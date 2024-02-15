using System.Reflection;
using Azure;
using FaqApp.Application.KnowledgeBase.Brokers;
using FaqApp.Application.KnowledgeBase.Services;
using FaqApp.Infrastructure.KnowledgeBase.Brokers;
using FaqApp.Infrastructure.KnowledgeBase.Services;
using FaqApp.Infrastructure.KnowledgeBase.Settings;

namespace FaqApp.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures text analysis infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddAzureLanguageServices(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<AzureAiServiceApiSettings>(builder.Configuration.GetSection(nameof(AzureAiServiceApiSettings)));
        builder.Services.Configure<AzureServiceSettings>(builder.Configuration.GetSection(nameof(AzureServiceSettings)));
        var azureAiServiceApiSettings = builder.Configuration.GetSection(nameof(AzureAiServiceApiSettings)).Get<AzureAiServiceApiSettings>()!;

        // Register brokers
        builder.Services.AddHttpClient<IKnowledgeBaseApiBroker, KnowledgeBaseApiBroker>(
            client =>
            {
                client.DefaultRequestHeaders.Add(azureAiServiceApiSettings.ApiKeyHeaderName, azureAiServiceApiSettings.ApiKey);
            }
        );
        
        // Register services
        builder.Services.AddScoped<IKnowledgeBaseService, KnowledgeBaseService>();

        return builder;
    }

    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}