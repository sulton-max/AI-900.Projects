using BikeUsers.Application.BikeUsages.Brokers;
using BikeUsers.Infrastructure.BikeUsages.Brokers;
using BikeUsers.Infrastructure.Settings;

namespace BikeUsers.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="ArgumentNullException">If api settings not found</exception>
    /// <returns></returns>
    private static WebApplicationBuilder AddBikeUsageInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<BikeUsagePredictionApiSettings>(builder.Configuration.GetSection(nameof(BikeUsagePredictionApiSettings)));
        var bikeUsageApiSettings = builder.Configuration.GetSection(nameof(BikeUsagePredictionApiSettings)).Get<BikeUsagePredictionApiSettings>() ??
                                   throw new ArgumentNullException(nameof(BikeUsagePredictionApiSettings));

        // Register interceptors
        builder.Services.AddTransient<AuthenticationHandler>();
        
        // Register http clients
        builder.Services.AddHttpClient<IBikeUsagePredictionApiBroker, BikeUsagePredictionApiBroker>(
                client => { client.BaseAddress = new Uri(bikeUsageApiSettings.BaseAddress); }
            )
            .AddHttpMessageHandler<AuthenticationHandler>();

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