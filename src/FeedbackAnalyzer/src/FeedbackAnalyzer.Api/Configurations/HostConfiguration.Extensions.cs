using System.Reflection;
using Azure;
using Azure.AI.TextAnalytics;
using FeedbackAnalyzer.Application.Common.TextAnalyzers.Brokers;
using FeedbackAnalyzer.Application.Ratings.Services;
using FeedbackAnalyzer.Infrastructure.Common.TextAnalyzers.Brokers;
using FeedbackAnalyzer.Infrastructure.Common.TextAnalyzers.Settings;
using FeedbackAnalyzer.Infrastructure.Ratings.Services;
using Microsoft.Extensions.Options;

namespace FeedbackAnalyzer.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Configures AutoMapper for object-to-object mapping using the specified profile.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }

    /// <summary>
    /// Configures text analysis infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddTextAnalysisInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<AzureLanguageServiceApiSettings>(builder.Configuration.GetSection(nameof(AzureLanguageServiceApiSettings)));

        // Register brokers
        builder.Services.AddKeyedScoped<TextAnalyticsClient>(
                "RatingAnalysis",
                (provider, serviceKey) =>
                {
                    var azureLanguageServiceApiSettings = provider.GetRequiredService<IOptions<AzureLanguageServiceApiSettings>>().Value;

                    if (serviceKey is "RatingAnalysis")
                        return new TextAnalyticsClient(
                            new Uri(azureLanguageServiceApiSettings.BaseAddress),
                            new AzureKeyCredential(azureLanguageServiceApiSettings.ApiKey)
                        );

                    throw new InvalidOperationException("No ComputerVisionClient registered for given key.");
                }
            )
            .AddScoped<ITextAnalyzerApiBroker, TextAnalyzerApiBroker>();

        return builder;
    }

    /// <summary>
    /// Configures rating infrastructure
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>>
    private static WebApplicationBuilder AddFeedbackInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRatingProcessingService, RatingProcessingService>();

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