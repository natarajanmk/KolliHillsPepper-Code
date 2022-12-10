using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;
using MediatR;

namespace KH.Pepper.Web
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //#: Register MediatR, where the services is located
            var assembly = AppDomain.CurrentDomain.Load("KH.Pepper.Core.AppServices");
            services.AddMediatR(assembly);

            //Register all services
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfRepository>();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
