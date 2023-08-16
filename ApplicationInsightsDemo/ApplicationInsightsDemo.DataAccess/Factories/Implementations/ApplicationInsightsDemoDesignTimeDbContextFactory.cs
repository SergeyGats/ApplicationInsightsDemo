using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApplicationInsightsDemo.DataAccess.Factories.Implementations
{
    public class ApplicationInsightsDemoDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationInsightsDemoDbContext>
    {
        private const string AspNetCoreEnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";
        private const string AppSettingsJsonFileName = "appsettings.json";
        private const string ConnectionStringName = "ApplicationInsightsDemo";
        private const string WebApiProjectDirectoryName = "ApplicationInsightsDemo.WebApi";

        public ApplicationInsightsDemoDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentVariableName);

            var webApiProjectPath = GetWebApiProjectPath();
            var environmentAppSettingsJsonFileName = GetEnvironmentAppSettingsJsonFileName(environment);

            var builder = new ConfigurationBuilder()
                .SetBasePath(webApiProjectPath)
                .AddJsonFile(AppSettingsJsonFileName, false, true)
                .AddJsonFile(environmentAppSettingsJsonFileName, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationInsightsDemoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new ApplicationInsightsDemoDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private string GetWebApiProjectPath()
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectoryPath = Directory.GetParent(currentDirectoryPath).FullName;
            var webApiProjectPath = Path.Combine(parentDirectoryPath, WebApiProjectDirectoryName);

            return webApiProjectPath;
        }

        private string GetEnvironmentAppSettingsJsonFileName(string environment)
        {
            return $"appsettings.{environment}.json";
        }
    }
}