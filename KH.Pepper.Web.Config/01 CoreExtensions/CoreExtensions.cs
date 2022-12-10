using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using KH.Pepper.Core.AppServices;

namespace KH.Pepper.Web.Config
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreExtensions(this IServiceCollection services,IConfiguration configuration,IHostEnvironment env)
        {
            
            services.AddDbContextBindings(configuration, env);

            //TODO
            services.AddMediatorBindings();

            services.AddRepositoryAndServiceBindings();

            //TODO - OK
            services.AddApplicationConfiguration(configuration);
             
            //TODO - OK
            services.AddValidatorsFromAssemblyContaining<GetApplicationIdentity>();

            return services;
        }
    }
}
