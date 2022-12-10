using KH.Pepper.Infra.DataBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.Pepper.Web.Config
{
    public static class HealthMonitoringExtension
    {
        public static IServiceCollection AddHealthMonitoring(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            //builder..AddDbContextCheck<AppDbContext>();
            return services;
        }
    }
}
