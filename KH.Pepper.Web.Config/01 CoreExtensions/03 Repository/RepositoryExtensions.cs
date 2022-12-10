using KH.Pepper.Core.AppServices.JwtTokenManager;
using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace KH.Pepper.Web.Config
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryAndServiceBindings(this IServiceCollection services)
        {
            services.AddScoped<IAddToCartRepository, AddToCartRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IProductOrderDetailsRepository, ProductOrderDetailsRepository>();
            services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
            services.AddScoped<IProductQuantityRepository, ProductQuantityRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
