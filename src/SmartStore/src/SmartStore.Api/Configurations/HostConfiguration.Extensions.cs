using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Extensions.Options;
using SmartStore.Application.Common.ContentAnalysis.Brokers;
using SmartStore.Application.Common.StorageFiles.Services;
using SmartStore.Infrastructure.Common.ContentAnalysis.Brokers;
using SmartStore.Infrastructure.Common.ContentAnalysis.Settings;
using SmartStore.Infrastructure.Common.StorageFiles.Services;

namespace SmartStore.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddContentAnalysisInfrastructure(this WebApplicationBuilder builder)
    {
        // Register brokers
        builder.Services.Configure<ImageAnalysisApiSettings>(builder.Configuration.GetSection(nameof(ImageAnalysisApiSettings)));

        // Register brokers
        builder.Services.AddScoped<ImageAnalysisClient>(
                provider =>
                {
                    var imageAnalysisApiSettings = provider.GetRequiredService<IOptions<ImageAnalysisApiSettings>>().Value;
                    return new ImageAnalysisClient(
                        new Uri(imageAnalysisApiSettings.BaseAddress),
                        new AzureKeyCredential(imageAnalysisApiSettings.ApiKey)
                    );
                }
            )
            .AddScoped<IImageAnalysisApiClient, ImageAnalysisApiClient>();

        return builder;
    }

    private static WebApplicationBuilder AddStorageFileInfrastructure(this WebApplicationBuilder builder)
    {
        // Register services
        builder.Services.AddScoped<IStorageFileProcessingService, StorageFileProcessingService>();

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