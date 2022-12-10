using Microsoft.Extensions.DependencyInjection;


namespace KH.Pepper.Web.Config
{
    public static class AntiForgeryConfigExtension
    {

        public static IServiceCollection AddAntiForgeryBindings(this IServiceCollection services)
        {

            services.AddAntiforgery(opt =>
            {
                opt.HeaderName = "X-XSRF-TOKEN";
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            });

            return services;
        }
    }
}
