using Microsoft.ApplicationInsights.DependencyCollector;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions;

public static class ApplicationInsightsConfigurationExtensions
{
    public static void AddApplicationInsightsConfiguration(this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry();

        //builder.Logging.AddApplicationInsightsConfiguration(
        //    configureTelemetryConfiguration: (config) =>
        //        config.ConnectionString = connectionString,
        //    configureApplicationInsightsLoggerOptions: (options) => { }
        //);

        //builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("*", LogLevel.Trace);

        services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
    }
}