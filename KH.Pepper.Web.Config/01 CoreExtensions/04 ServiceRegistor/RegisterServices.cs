using KH.Pepper.Core.AppServices.JwtTokenManager;
using Microsoft.Extensions.DependencyInjection;


namespace KH.Pepper.Web.Config
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Register all services
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            
            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
