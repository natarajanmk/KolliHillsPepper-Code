using KH.Pepper.Core.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KH.Pepper.Web.Config
{
    public static class ExceptionHandlingExtensions
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions<ExceptionHandlingOptions>()
                .Configure(o =>
                {
                    o.Map<EntityNotFoundException>((context, ex) => new ProblemDetailsMapping
                    {
                        Title = ex.Message,
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
                });

            return services;
        }
    }
}
