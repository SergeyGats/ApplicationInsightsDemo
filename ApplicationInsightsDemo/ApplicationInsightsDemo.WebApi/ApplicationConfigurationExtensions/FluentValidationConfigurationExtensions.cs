using FluentValidation.AspNetCore;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class FluentValidationConfigurationExtensions
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}