using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Extensions.Options;
using ProfileUpdater.Application.Common.Identity.Services;
using ProfileUpdater.Application.Common.ImageFaceAnalysis.Brokers;
using ProfileUpdater.Infrastructure.Common.Identity.Services;
using ProfileUpdater.Infrastructure.Common.ImageFaceAnalysis.Brokers;
using ProfileUpdater.Infrastructure.Common.ImageFaceAnalysis.Settings;

namespace ProfileUpdater.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddImageAnalysisServices(this WebApplicationBuilder builder)
    {
        // Register configurations
        builder.Services.Configure<FaceRecognitionApiSettings>(builder.Configuration.GetSection(nameof(FaceRecognitionApiSettings)));

        // Register brokers
        builder.Services.
            AddKeyedScoped<IFaceClient, FaceClient>(
                "ProfilePhotoAnalysis",
                (provider, serviceKey) =>
                {
                    var faceAnalysisApiSettings = provider.GetRequiredService<IOptions<FaceRecognitionApiSettings>>().Value;

                    if (serviceKey is "ProfilePhotoAnalysis")
                        return new FaceClient(new ApiKeyServiceClientCredentials(faceAnalysisApiSettings.ApiKey))
                        {
                            Endpoint = faceAnalysisApiSettings.BaseAddress,
                        };

                    throw new InvalidOperationException("No FaceClient registered for given key.");
                }
            )
            .AddScoped<IFaceRecognitionApiBroker, FaceRecognitionApiBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // Register services
        builder.Services.AddScoped<IAccountAggregationService, AccountAggregationService>();

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