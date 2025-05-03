using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiATON.Features.Filters
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authorizeAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .ToList();

            if (authorizeAttributes.Any())
            {
                operation.Description += " (Требуется авторизация)";

                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }

            var adminRoles = authorizeAttributes
                .Where(attr => attr.Roles?.Contains("Admin") == true)
                .ToList();

            if (adminRoles.Any())
            {
                operation.Description += " (Только для администраторов)";
            }
        }
    }
}