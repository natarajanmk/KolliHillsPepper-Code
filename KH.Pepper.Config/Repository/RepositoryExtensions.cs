using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace KH.Pepper.Config
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Add binding all repository here..
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryBindings(this IServiceCollection services)
        {
            services.AddScoped<IAddToCartRepository, AddToCartRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IProductOrderDetailsRepository, ProductOrderDetailsRepository>();
            services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
            services.AddScoped<IProductQuantityRepository, ProductQuantityRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
