using ApplicationInsightsDemo.Configuration.Models;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class ApplicationSettingsConfigurationExtensions
    {
        public static CommonApplicationSettings AddApplicationSettingsConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var commonApplicationSettingsSection = configuration.GetSection(nameof(CommonApplicationSettings));
            services.Configure<CommonApplicationSettings>(commonApplicationSettingsSection);
            var commonApplicationSettings = commonApplicationSettingsSection.Get<CommonApplicationSettings>();

            var jwtAuthenticationSettingsSection = configuration.GetSection(nameof(JwtAuthenticationSettings));
            services.Configure<JwtAuthenticationSettings>(jwtAuthenticationSettingsSection);

            return commonApplicationSettings;
        }
    }
}