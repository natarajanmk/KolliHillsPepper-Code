using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KH.Pepper.Web.Config
{
    public static class AuthorizationConfigExtension
    {

        public static IServiceCollection AddAuthorizationBindings(this IServiceCollection services, IConfiguration configuration)
        {
            typeof(AuthorizationConfigExtension).Assembly
                .GetTypesAssignableFrom<IAuthorizationHandler>()
                .Where(x => x.IsAbstract == false)
                .ToList()
                .ForEach(t =>
                {
                    services.AddTransient(typeof(IAuthorizationHandler), t);
                });
                
            return services;
        }

        private static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly)
        {
            return assembly.GetTypesAssignableFrom(typeof(T));
        }
        private static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
        {
            List<Type> ret = new List<Type>();
            foreach(var type in assembly.DefinedTypes)
            {
                if(compareType.IsAssignableFrom(type) && compareType != type)
                {
                    ret.Add(type);
                }
            }
            return ret;
        }
    }
}
