using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Options;
using ResumeScanner.Application.Common.Caching.Brokers;
using ResumeScanner.Application.Common.DocumentProcessing.Brokers;
using ResumeScanner.Application.Common.StorageFiles.Brokers;
using ResumeScanner.Application.Resumes.Services;
using ResumeScanner.Infrastructure.Common.Caching.Brokers;
using ResumeScanner.Infrastructure.Common.DocumentProcessing.Brokers;
using ResumeScanner.Infrastructure.Common.DocumentProcessing.Settings;
using ResumeScanner.Infrastructure.Common.StorageFiles.Brokers;
using ResumeScanner.Infrastructure.Resumes.Services;

namespace ResumeScanner.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures caching
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICacheBroker, CacheBroker>();

        return builder;
    }

    /// <summary>
    /// Configures storage files infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddStorageFilesInfrastructure(this WebApplicationBuilder builder)
    {
        // Register brokers
        builder.Services.AddTransient<IFileChecksumProvider, FileChecksumProvider>();

        return builder;
    }

    /// <summary>
    /// Configures document analysis infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddDocumentAnalysisInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<AzureCognitiveServiceApiSettings>(builder.Configuration.GetSection(nameof(AzureCognitiveServiceApiSettings)));

        // Register brokers
        builder.Services.AddKeyedScoped<IComputerVisionClient, ComputerVisionClient>(
            "DocumentAnalysis",
            (provider, serviceKey) =>
            {
                var azureCognitiveServiceApiSettings = provider.GetRequiredService<IOptions<AzureCognitiveServiceApiSettings>>().Value;

                if (serviceKey is "DocumentAnalysis")
                    return new ComputerVisionClient(new ApiKeyServiceClientCredentials(azureCognitiveServiceApiSettings.ApiKey))
                    {
                        Endpoint = azureCognitiveServiceApiSettings.BaseAddress,
                    };

                throw new InvalidOperationException("No ComputerVisionClient registered for given key.");
            }
        );

        builder.Services.AddScoped<IDocumentScannerApiBroker, DocumentScannerApiBroker>();

        return builder;
    }

    /// <summary>
    /// Configures resumes infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddResumesInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResumeProcessingService, ResumeProcessingService>();

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