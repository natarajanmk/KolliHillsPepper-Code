using KH.Pepper.Core.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace KH.Pepper.Web.Config
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiExtensions(this IServiceCollection services,IConfiguration configuration,IHostEnvironment env)
        {
            //TODO
            services.AddAuthenticationBindings(configuration, env);

            services.AddAuthorizationBindings(configuration);

            services.AddAntiForgeryBindings();
            
            services.AddSwaggerBindings();

            //JWT Token settings
            services.AddJwtTokenBindings(configuration);

            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            });

            services.AddExceptionHandling();

            services.AddSingleton<IUserSessionProvider, ApiUserSessionProvider>();

            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.Forbidden));
                opt.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest));
                opt.Filters.Add(new ProducesDefaultResponseTypeAttribute());
            })
            .AddNewtonsoftJson(opt =>
            {
                opt.UseCamelCasing(false);
                opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });

            return services;
        }
    }
}
