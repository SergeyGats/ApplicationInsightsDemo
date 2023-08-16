using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Implementations;
using ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions;
using ApplicationInsightsDemo.WebApi.Constants;
using ApplicationInsightsDemo.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var serviceProvider = services.BuildServiceProvider();
var configuration = serviceProvider.GetRequiredService<IConfiguration>();

services.AddControllers();

var commonApplicationSettings = services.AddApplicationSettingsConfiguration(configuration);

services.AddAutoMapperConfiguration();
services.AddDbContextConfiguration(configuration, commonApplicationSettings);
services.AddDependencyInjectionConfiguration();
services.AddFluentValidationConfiguration();
//services.AddNlogConfiguration();
services.AddSwaggerConfiguration();
services.AddApplicationInsightsConfiguration();
services.AddKeyVaultConfiguration(builder, configuration);

var app = builder.Build();

if (commonApplicationSettings.IsEnableSwaggerUi)
{
    UseSwaggerUi();
}

if (commonApplicationSettings.IsApplyDatabaseMigrationsOnStartup)
{
    ApplyDatabaseMigrations();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAzureAppConfiguration();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(configure =>
{
    configure.MapControllers();
});

app.Run();

void UseSwaggerUi()
{
    var currentAppVersion = "TEST app version";
    var swaggerEndpointWithCurrentVersion = string.Format(WebApiConstants.SwaggerEndpointUrl, currentAppVersion);

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, WebApiConstants.ProjectName));
}

void ApplyDatabaseMigrations()
{
    using var scope = serviceProvider.CreateAsyncScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationInsightsDemoDbContext>();
    context.Database.Migrate();
}
