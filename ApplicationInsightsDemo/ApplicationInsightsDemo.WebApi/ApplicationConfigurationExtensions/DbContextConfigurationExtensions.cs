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
            CommonApplicationSettings commonApplicationSettings,
            DatabaseConnectionStrings databaseConnectionStrings)
        {
            //var connectionString = configuration.GetConnectionString(ConnectionStringName);
            var connectionString = databaseConnectionStrings.ApplicationInsightsDemo;

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