using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KH.Pepper.Web.Config
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContextBindings(this IServiceCollection services,IConfiguration configuration,IHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");

            Console.WriteLine($"Running on {env.EnvironmentName}");
             
            //services.AddDbContext<AppDbContext>(opt =>
            //{
            //    opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("KH.Pepper.Infra.DataBase"));
            //    //opt.UseSqlServer(connectionString, b => { });
            //});
            
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("KH.Pepper.Web")), ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<AppDbContext>());
            
            return services;
        }
    }
}
