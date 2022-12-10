using KH.Pepper.Core.AppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KH.Pepper.Config
{
    public static class ApplicationConfigurationExtension
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new ApplicationConfiguration();
            configuration.Bind(nameof(ApplicationConfiguration), config);
            config.ApplicationInsightsConnectionString = configuration.GetConnectionString("ApplicationInsights");
            services.AddSingleton(config);

            return services;
        }
    }
}
