using KH.Pepper.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace KH.Pepper.Web
{
    public class ErrorHandlingMiddleware1
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware1(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // This is nothing but switch case and assign response.StatusCode value
            response.StatusCode = exception switch
            {
                ExceptionHandle _ => (int)HttpStatusCode.BadRequest,
                ItemNotFoundException _ => (int)HttpStatusCode.NotFound,
                ValidationException _ => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,// default unhandled error
            };

            return response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseModel(exception)));
        }
    }

    //public static class ExceptionHandlingMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    //    {
    //        return app.UseMiddleware<ErrorHandlingMiddleware>();
    //    }
    //}
}
