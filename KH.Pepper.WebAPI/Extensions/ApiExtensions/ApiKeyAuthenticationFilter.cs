
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KH.Pepper.Web.ApiExtensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    //IAsyncActionFilter: Microsoft.AspNetCore.Mvc.Filters
    public class ApiKeyAuthenticationFilter : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyAuthenticationFilterName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before filter : validate the api key authentication
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyAuthenticationFilterName, out var extractedApiKey))
            {
                //ContentResult:Microsoft.AspNetCore.Mvc
                context.Result = new Microsoft.AspNetCore.Mvc.ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key not provided"
                };
                return;
            }
            //IConfiguration: Microsoft.Extensions.Configuration
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKeyAuthenticationFilterName);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }
            await next();
        }
    }
}