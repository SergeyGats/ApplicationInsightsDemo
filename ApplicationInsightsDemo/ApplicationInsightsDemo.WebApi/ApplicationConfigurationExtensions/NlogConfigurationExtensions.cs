using NLog.Extensions.Logging;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class NlogConfigurationExtensions
    {
        public static void AddNlogConfiguration(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });
        }
    }
}