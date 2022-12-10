using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KH.Pepper.Web.ApiExtensions
{
    //AddApiCustomRequestHeader
    public class ApiRequestHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //OpenApiParameter: Microsoft.OpenApi.Models
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            //ControllerActionDescriptor: Microsoft.AspNetCore.Mvc.Controllers
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null && descriptor.ControllerName.StartsWith("Login"))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Description = "Api key authentication",
                    Required = true,
                });
            }
        }
    }
}
