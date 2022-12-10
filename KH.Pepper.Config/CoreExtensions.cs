using FluentValidation;
using KH.Pepper.Config.MediatR;
using KH.Pepper.Core.AppServices;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KH.Pepper.Config
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreExtensions(this IServiceCollection services,IConfiguration configuration,IHostEnvironment env)
        {
            
            services.AddDbContextBindings(configuration, env);
            services.AddRepositoryBindings();
            services.AddApplicationConfiguration(configuration);
             
            //TODO
            //services.AddMediatorBindings();
            //services.AddValidatorsFromAssemblyContaining<GetApplicationIdentity>();


            return services;
        }
    }
}
