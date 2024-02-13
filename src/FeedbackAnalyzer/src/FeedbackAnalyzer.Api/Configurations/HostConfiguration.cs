namespace FeedbackAnalyzer.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures application builder
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns>Application builder</returns>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMappers()
            .AddTextAnalysisInfrastructure()
            .AddFeedbackInfrastructure()
            .AddDevTools()
            .AddExposers();

        return new ValueTask<WebApplicationBuilder>(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseExposers();
        
        return ValueTask.FromResult(app);
    }
}