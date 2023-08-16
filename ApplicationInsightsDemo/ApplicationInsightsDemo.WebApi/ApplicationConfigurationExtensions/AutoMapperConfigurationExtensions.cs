using System.Reflection;
using ApplicationInsightsDemo.BusinessLogic;
using ApplicationInsightsDemo.DataAccess;

namespace ApplicationInsightsDemo.WebApi.ApplicationConfigurationExtensions
{
    public static class AutoMapperConfigurationExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var assemblies = new List<Assembly>
            {
                typeof(ApplicationInsightsDemoWebApiAssemblyReference).Assembly,
                typeof(ApplicationInsightsDemoBusinessLogicAssemblyReference).Assembly,
                typeof(ApplicationInsightsDemoDataAccessAssemblyReference).Assembly
            };

            services.AddAutoMapper(assemblies);
        }
    }
}