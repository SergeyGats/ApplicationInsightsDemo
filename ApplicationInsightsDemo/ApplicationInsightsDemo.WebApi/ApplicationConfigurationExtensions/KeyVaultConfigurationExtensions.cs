using ApplicationInsightsDemo.WebApi.Options;
using Azure.Identity;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions;

public static class KeyVaultConfigurationExtensions
{
    public static void AddKeyVaultConfiguration(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
    {
        try
        {
            builder.Host.ConfigureAppConfiguration(options =>
          {
              var azureAppConfigurationConnectionStringName = "AzureAppConfiguration";
              var azureAppConfigurationConnectionString = configuration.GetConnectionString(azureAppConfigurationConnectionStringName);

              options.AddAzureAppConfiguration(appConfigurationOptions =>
              {
                  appConfigurationOptions.Connect(azureAppConfigurationConnectionString);
                  appConfigurationOptions.ConfigureKeyVault(keyVaultOptions =>
                  {
                      keyVaultOptions.SetCredential(new DefaultAzureCredential());
                      keyVaultOptions.SetSecretRefreshInterval(TimeSpan.FromSeconds(15));
                  }).ConfigureRefresh(refreshOptions =>
                  {
                      refreshOptions.Register($"{AppConfOptions.AppConfOptionsKey}:{nameof(AppConfOptions.FirstConfValue)}")
                          .SetCacheExpiration(TimeSpan.FromSeconds(15));

                      refreshOptions.Register($"{AppConfOptions.AppConfOptionsKey}:{nameof(AppConfOptions.SecondConfValue)}")
                          .SetCacheExpiration(TimeSpan.FromSeconds(15));
                  });
              });
          });

            services.AddAzureAppConfiguration();
            var hz1 = configuration.GetSection(AppConfOptions.AppConfOptionsKey);
            var hz2 = configuration.GetSection("FirstConfiguration");

            services.Configure<AppConfOptions>(builder.Configuration.GetSection(AppConfOptions.AppConfOptionsKey));
            //services.Configure<AppConfOptions>(builder.Configuration.GetSection("FirstConfiguration"));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}