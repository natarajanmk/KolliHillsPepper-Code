using KH.Pepper.Core.AppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using System.Security;

namespace KH.Pepper.Web.Config
{
    public static class AuthenticationConfigExtension
    {
        public static IServiceCollection AddAuthenticationBindings(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var authConfig = new AuthenticationConfiguration();
            configuration.Bind(nameof(AuthenticationConfiguration), authConfig);
            services.AddSingleton(authConfig);

            services.AddSingleton<IUserSessionProvider, ApiUserSessionProvider>();

            if (env.IsDevelopment() && authConfig.UseDevelopmentAuthentication)
            {
                //services.AddAuthentication("Development")..AddDevelopmentAuthentication(options =>
                //{
                //    options.DevelopmentClaimBuilder.UserCode = "123456";
                //    options.DevelopmentClaimBuilder.AddClaim(new Claim()
                //})
            }

           // services.AddAuthentication("AppServices").AddAppServiceAuthentication();

            return services;
        }
    }
}
