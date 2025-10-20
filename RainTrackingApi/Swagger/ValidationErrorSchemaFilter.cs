using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RainTrackingApi.Models.Response;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RainTrackingApi.Swagger
{
    public class ValidationErrorSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Replace Microsoft.AspNetCore.Mvc.ProblemDetails with our custom ValidationErrorResponse
            if (context.Type == typeof(ProblemDetails))
            {
                var validationErrorSchema = context.SchemaGenerator.GenerateSchema(typeof(ValidationErrorResponse), context.SchemaRepository);
                
                schema.Type = validationErrorSchema.Type;
                schema.Properties = validationErrorSchema.Properties;
                schema.Required = validationErrorSchema.Required;
                schema.Example = validationErrorSchema.Example;
                schema.Description = "Validation error response containing details about failed request validation";
            }
        }
    }
}