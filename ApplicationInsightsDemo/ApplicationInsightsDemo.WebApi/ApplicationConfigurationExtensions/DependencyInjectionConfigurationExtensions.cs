using ApplicationInsightsDemo.BusinessLogic.Helpers.Implementations;
using ApplicationInsightsDemo.BusinessLogic.Helpers.Interfaces;
using ApplicationInsightsDemo.BusinessLogic.Services.Implementations;
using ApplicationInsightsDemo.BusinessLogic.Services.Interfaces;
using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Implementations;
using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Interfaces;
using ApplicationInsightsDemo.WebApi.Helpers.Implementations;
using ApplicationInsightsDemo.WebApi.Helpers.Interfaces;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class DependencyInjectionConfigurationExtensions
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            ConfigureWebApiDependencyInjection(services);
            ConfigureBusinessLogicDependencyInjection(services);
            ConfigureDataAccessDependencyInjection(services);
        }

        private static void ConfigureWebApiDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        private static void ConfigureBusinessLogicDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IPasswordHelper, PasswordHelper>();

            services.AddScoped<IUserService, UserService>();
        }

        private static void ConfigureDataAccessDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IApplicationInsightsDemoDbContext, ApplicationInsightsDemoDbContext>();
        }
    }
}