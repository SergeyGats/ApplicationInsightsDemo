namespace ApplicationInsightsDemo.Configuration.Models
{
    public class CommonApplicationSettings
    {
        public bool IsEnableSwaggerUi { get; set; }
        public bool IsEnableEntityFrameworkSensitiveDataLogging { get; set; }
        public bool IsApplyDatabaseMigrationsOnStartup { get; set; }
    }
}