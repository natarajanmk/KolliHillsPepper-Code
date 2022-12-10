using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KH.Pepper.Web.Config
{
    public static class ApiExtensionsBindings
    {
        public static IServiceCollection AddExtensionBindings(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            //01 Core Extensions
            services.AddCoreExtensions(configuration, env);

            //02 Api Extensions
            services.AddApiExtensions(configuration, env);
             
            return services;
        }
    }
}
