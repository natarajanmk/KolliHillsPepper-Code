using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace KH.Pepper.Config
{
    public static class DbContextBindingExtensions
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

             
            // options.UseSqlServer(connection, b => b.MigrationsAssembly("KH.Pepper.Web"))
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("KH.Pepper.Web")), ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<AppDbContext>());
            
            return services;
        }
    }
}
