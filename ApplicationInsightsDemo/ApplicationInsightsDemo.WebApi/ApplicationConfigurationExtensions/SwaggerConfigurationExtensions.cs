using Microsoft.OpenApi.Models;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class SwaggerConfigurationExtensions
    {
        private const string BearerSecurityDefinitionDescription =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n " +
            "Example: \"Bearer 1safsfsdfdfd\"";

        private const string AuthorizationSecuritySchemeName = "Authorization";
        private const string BearerOpenApiReferenceId = "Bearer";
        private const string BearerSecurityDefinitionName = "Bearer";

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                var currentAppVersion = "TEST app version";
                options.SwaggerDoc(currentAppVersion,
                    new OpenApiInfo
                    {
                        Title = "TEST project name",
                        Version = currentAppVersion
                    });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = BearerSecurityDefinitionDescription,
                    Name = AuthorizationSecuritySchemeName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new OpenApiReference
                    {
                        Id = BearerOpenApiReferenceId,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(BearerSecurityDefinitionName, securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                { securitySchema, new[] { "Bearer" } }
                };

                options.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}