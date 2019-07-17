using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyality
{
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
         public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }
            Boolean isAuthorized = context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(x => x.Filter is AuthorizeFilter);
            Boolean isAllowAnonimous = context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(x => x.Filter is AllowAnonymousFilter);

            if (!isAuthorized || isAllowAnonimous) return;

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "Authorization",
                In = "header",
                Description = "access token",
                Required = true,
                Type = "string",
                Default = "Bearer "
            });
        }
    }
}
