using ApplicationInsightsDemo.Configuration.Models;
using Azure.Identity;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions;

public static class KeyVaultConfigurationExtensions
{
    private const string ConnectionStringName = "AzureAppConfiguration";

    private const int SecretRefreshIntervalSecondsNumber = 15;
    private static readonly TimeSpan SecretRefreshIntervalTimeSpan = TimeSpan.FromSeconds(SecretRefreshIntervalSecondsNumber);

    private const int CacheExpirationSecondsNumber = 15;
    private static readonly TimeSpan CacheExpirationTimeSpan = TimeSpan.FromSeconds(CacheExpirationSecondsNumber);

    public static DatabaseConnectionStrings AddKeyVaultConfiguration(this IServiceCollection services,
        WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Host.ConfigureAppConfiguration(options =>
        {
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            options.AddAzureAppConfiguration(appConfigurationOptions =>
            {
                appConfigurationOptions.Connect(connectionString);
                appConfigurationOptions.ConfigureKeyVault(keyVaultOptions =>
                {
                    keyVaultOptions.SetCredential(new DefaultAzureCredential());
                    keyVaultOptions.SetSecretRefreshInterval(SecretRefreshIntervalTimeSpan);
                }).ConfigureRefresh(refreshOptions =>
                {
                    refreshOptions.Register($"{nameof(FirstFeatureSettings)}:{nameof(FirstFeatureSettings.StartDate)}")
                        .SetCacheExpiration(CacheExpirationTimeSpan);

                    refreshOptions.Register($"{nameof(FirstFeatureSettings)}:{nameof(FirstFeatureSettings.EndDate)}")
                        .SetCacheExpiration(CacheExpirationTimeSpan);

                    refreshOptions.Register($"{nameof(FirstFeatureSettings)}:{nameof(FirstFeatureSettings.Message)}")
                        .SetCacheExpiration(CacheExpirationTimeSpan);

                    refreshOptions.Register($"{nameof(DatabaseConnectionStrings)}:{nameof(DatabaseConnectionStrings.ApplicationInsightsDemo)}")
                        .SetCacheExpiration(CacheExpirationTimeSpan);
                });
            });
        });

        services.AddAzureAppConfiguration();

        services.Configure<FirstFeatureSettings>(builder.Configuration.GetSection(nameof(FirstFeatureSettings)));

        var databaseConnectionStringsSection = builder.Configuration.GetSection(nameof(DatabaseConnectionStrings));
        services.Configure<DatabaseConnectionStrings>(databaseConnectionStringsSection);
        var databaseConnectionString = databaseConnectionStringsSection.Get<DatabaseConnectionStrings>();

        return databaseConnectionString;
    }
}