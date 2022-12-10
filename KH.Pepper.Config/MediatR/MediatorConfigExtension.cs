using KH.Pepper.Core.AppServices;
using KH.Pepper.Core.AppServices.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KH.Pepper.Config.MediatR
{
    public static class MediatorConfigExtension
    {
        public static IServiceCollection AddMediatorBindings(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddMediatR(typeof(GetApplicationIdentity).Assembly);

             
            return services;
        }
    }
}
