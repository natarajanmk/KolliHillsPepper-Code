using KH.Pepper.Core.AppServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KH.Pepper.Web.Config
{
    public static class MediatorExtension
    {
        public static IServiceCollection AddMediatorBindings(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            //#: Register MediatR, where the services is located
            //var assembly = AppDomain.CurrentDomain.Load("KH.Pepper.Core.AppServices");
            //services.AddMediatR(assembly);

            //var assembly = AppDomain.CurrentDomain.Load("KH.Pepper.Core.AppServices");
            //services.AddMediatR(assembly);

            services.AddMediatR(typeof(GetApplicationIdentity).Assembly);
                         
            return services;
        }
    }
}
