using FeedbackAnalysis.Application.Common.ContentSafety.Brokers;
using FeedbackAnalysis.Application.Common.Serializers;
using FeedbackAnalysis.Infrastructure.Common.ContentSafety.Brokers;
using FeedbackAnalysis.Infrastructure.Common.ContentSafety.Settings;
using FeedbackAnalysis.Infrastructure.Common.Serializers;

namespace FeedbackAnalysis.Api.Configurations;

public static partial class HostConfiguration
{
    
    /// <summary>
    /// Configures and adds Serializers to web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        // register json serialization settings
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();

        return builder;
    }
    
    /// <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="ArgumentNullException">If api settings not found</exception>
    /// <returns></returns>
    private static WebApplicationBuilder AddBikeUsageInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<ContentSafetyApiSettings>(builder.Configuration.GetSection(nameof(ContentSafetyApiSettings)));
        builder.Services.Configure<ContentSafetyThresholdSettings>(builder.Configuration.GetSection(nameof(ContentSafetyThresholdSettings)));
        var contentSafetyApiSettings = builder.Configuration.GetSection(nameof(ContentSafetyApiSettings)).Get<ContentSafetyApiSettings>() ??
                                       throw new ArgumentNullException(nameof(ContentSafetyApiSettings));

        // Register http clients
        builder.Services.AddHttpClient<IContentSafetyApiBroker, ContentSafetyApiBroker>(
            httpClient =>
            {
                // Set base address
                httpClient.BaseAddress = new Uri(contentSafetyApiSettings.BaseAddress);

                // Set headers
                httpClient.DefaultRequestHeaders.Add(contentSafetyApiSettings.ApiKeyHeader, contentSafetyApiSettings.ApiKey);
            }
        );

        return builder;
    }

    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
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