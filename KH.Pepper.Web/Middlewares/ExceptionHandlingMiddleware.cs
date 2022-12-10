using FluentValidation;
using KH.Pepper.Core.Domain.Exceptions;
using KH.Pepper.Services;
using KH.Pepper.Web.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace KH.Pepper.Web
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly IOptionsMonitor<ExceptionHandlingOptions> _options;

         public ExceptionHandlingMiddleware(RequestDelegate next,
             ILogger<ExceptionHandlingMiddleware> logger, 
             IActionResultExecutor<ObjectResult> executor, 
             IOptionsMonitor<ExceptionHandlingOptions> options)
        {
            _next = next;
            _logger = logger;
            _executor = executor;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (httpContext.Response.HasStarted)
                {
                    _logger.LogError("Response has already started");
                    return;
                }
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        private static readonly RouteData EmptyRouteData = new RouteData();


        private async Task WriteHttpResponseAsync<T>(HttpContext context, T ex) where T : Exception
        {
            var problemDetail = TransformToProblemDetail(context, ex);

            var routeData = context.GetRouteData() ?? EmptyRouteData;
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);

            var result = new ObjectResult(problemDetail)
            {
                StatusCode = problemDetail.Status ?? (int)HttpStatusCode.InternalServerError,
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection
                {
                    "application/problem+json"
                },
                DeclaredType = problemDetail.GetType(),
            };

            await _executor.ExecuteAsync(actionContext, result);

            await context.Response.CompleteAsync();


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

        private ProblemDetails TransformToProblemDetail<T>(HttpContext httpContext, T ex) where T : Exception
        {
            switch (ex)
            {
                case DomainException domainException:
                    return TransformToDomainProblemDetail(httpContext, domainException);

                case ValidationException validationException:
                    return TransformToValidationProblemDetails(httpContext, validationException);
                default:
                    return new ProblemDetails
                    {
                        Instance = httpContext.Request.Path,
                        Detail = "an error has occurred",
                        Status = (int)HttpStatusCode.InternalServerError,
                    };
            }
        }

        private static ProblemDetails TransformToValidationProblemDetails(HttpContext httpContext,
            ValidationException validationException)
        {
            var errors = new Dictionary<string, string[]>();
            var state = new Dictionary<string, object>();

            foreach(var errorsPerPorperty in validationException.Errors.GroupBy(e => e.PropertyName))
            {
                //apply tocamelcase validation instead of tolower()
                var propertyName = string.Join(".", errorsPerPorperty.Key.Split(".").Select(x => x.ToLower()));

                errors.Add(
                    propertyName,
                    errorsPerPorperty.Select(er => er.ErrorMessage).ToArray());

                var statePerProperty = errorsPerPorperty
                    .Where(er => er.CustomState != null)
                    .Select(er => new KeyValuePair<string, object>(er.ErrorMessage, er.CustomState))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                if (statePerProperty.Values.Any())
                {
                    state.Add(propertyName, statePerProperty);
                }
            }
            var details = new ValidationProblemDetails(errors)
            {
                Title = "Validation errors occurred",
                Instance = httpContext.Request.Path,
                Detail = validationException.Message,
                Status = (int)HttpStatusCode.BadRequest
            };

            details.Extensions.Add("extensions", state);
            return details;
        }

        private ProblemDetails TransformToDomainProblemDetail(HttpContext httpContext, DomainException exception)
        {

            if(TryGetProblemDetail(httpContext,exception,out var problemDetails))
            {
                return problemDetails;
            }
            return new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
                Status = (int)HttpStatusCode.InternalServerError
            };
        }

        private bool TryGetProblemDetail(HttpContext context, DomainException ex, out ProblemDetails problemDetails)
        {
            problemDetails = null;
            if(_options.CurrentValue.Mappers.TryGetValue(ex.GetType(), out var mapper))
            {
                var mapping = mapper(context, ex);
                if(mapper != null)
                {
                    problemDetails = mapping.ToProblemDetails();
                    return true;
                }
            }
            return false;
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
