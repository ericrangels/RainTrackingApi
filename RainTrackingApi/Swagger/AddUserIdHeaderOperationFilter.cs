using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RainTrackingApi.Swagger
{
    public class AddUserIdHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            if (operation.Parameters.Any(p => p.Name == "x-userId" && p.In == ParameterLocation.Header))
                return;

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-userId",
                In = ParameterLocation.Header,
                Required = true,
                Description = "User identifier header. Add this to GET and POST calls.",
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
