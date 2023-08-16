using ApplicationInsightsDemo.Configuration.Models;
using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Implementations;
using Microsoft.EntityFrameworkCore;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class DbContextConfigurationExtensions
    {
        private const string ConnectionStringName = "ApplicationInsightsDemo";

        public static void AddDbContextConfiguration(this IServiceCollection services,
            IConfiguration configuration,
            CommonApplicationSettings commonApplicationSettings)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            services.AddDbContext<ApplicationInsightsDemoDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

                if (commonApplicationSettings.IsEnableEntityFrameworkSensitiveDataLogging)
                {
                    options.EnableSensitiveDataLogging();
                }
            });
        }
    }
}