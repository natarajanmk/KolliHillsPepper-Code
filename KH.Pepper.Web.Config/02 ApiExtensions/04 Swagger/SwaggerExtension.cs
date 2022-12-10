using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace KH.Pepper.Web.Config
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerBindings(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "KolliHillsPepper",
                    Description = "ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Natarajan M",
                        Email = "natarajanmk@gmail.com"
                    }
                });
                c.MapType<FileContentResult>(() => new OpenApiSchema
                {
                    Type = "file",
                });

                c.CustomOperationIds(x =>
                   x.TryGetMethodInfo(out MethodInfo methodInfo) ? $"{x.ActionDescriptor.RouteValues["controller"]}_{methodInfo.Name}" : null);
                c.CustomSchemaIds(DefaultSchemaIdSelector);
                c.SchemaFilter<ProblemDetailsFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Input your apikey to access all APIs",
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey",
                            },
                            Scheme = "ApiKey",
                            Name = "ApiKey",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        private static string DefaultSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType)
            {
                if (modelType.FullName != null && modelType.FullName.Contains("+"))
                {
                    string schemaName = string.Empty;
                    if (modelType.FullName != null && modelType.FullName.Contains("+"))
                    {
                        schemaName = modelType.FullName.Split(".").Last().Replace("+", "").Replace("[]", "Array");
                        return schemaName.StartsWith("Get") && schemaName.Length != 3 ? schemaName.Substring(3) : schemaName;
                    }

                    schemaName = modelType.Name.Replace("[]", "Array");
                    return schemaName.StartsWith("Get") && schemaName.Length != 3 ? schemaName.Substring(3) : schemaName;
                }
                return modelType.Name.Replace("[]", "Array");
            }

            var prefix = modelType.GetGenericArguments()
                .Select(DefaultSchemaIdSelector)
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name
                .Split('`')
                .First();
        }
    }

    public class ProblemDetailsFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null) return;

            var typeInfo = context.Type;

            if (typeof(ProblemDetails).IsAssignableFrom(typeInfo) || typeof(ValidationProblemDetails).IsAssignableFrom(typeInfo))
            {
                schema.Properties.Add("extensions", new OpenApiSchema
                {
                    Type = "object",
                    AdditionalProperties = new OpenApiSchema
                    {
                        Type = "object",
                        AdditionalPropertiesAllowed = false
                    },
                    Nullable = true,
                    ReadOnly = true
                });
            }
        }
    }

}
