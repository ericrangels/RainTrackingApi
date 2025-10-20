using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RainTrackingApi.Swagger
{
    public class CustomSchemaDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Replace ProblemDetails schema name with ValidationError
            if (swaggerDoc.Components?.Schemas?.ContainsKey("ProblemDetails") == true)
            {
                var problemDetailsSchema = swaggerDoc.Components.Schemas["ProblemDetails"];
                swaggerDoc.Components.Schemas.Remove("ProblemDetails");
                swaggerDoc.Components.Schemas["ValidationError"] = problemDetailsSchema;

                // Update all references to use the new schema name
                UpdateReferences(swaggerDoc, "#/components/schemas/ProblemDetails", "#/components/schemas/ValidationError");
            }
        }

        private void UpdateReferences(OpenApiDocument document, string oldRef, string newRef)
        {
            foreach (var path in document.Paths.Values)
            {
                foreach (var operation in path.Operations.Values)
                {
                    foreach (var response in operation.Responses.Values)
                    {
                        UpdateSchemaReferences(response.Content, oldRef, newRef);
                    }

                    if (operation.RequestBody?.Content != null)
                    {
                        UpdateSchemaReferences(operation.RequestBody.Content, oldRef, newRef);
                    }
                }
            }
        }

        private void UpdateSchemaReferences(IDictionary<string, OpenApiMediaType> content, string oldRef, string newRef)
        {
            foreach (var mediaType in content.Values)
            {
                var schema = mediaType.Schema;
                if (schema?.Reference != null)
                {
                    // OpenApiReference.ReferenceV3 is read-only, so replace the Reference object if needed
                    if (schema.Reference.ReferenceV3 == oldRef)
                    {
                        schema.Reference = new OpenApiReference
                        {
                            Type = schema.Reference.Type,
                            Id = newRef.Replace("#/components/schemas/", string.Empty)
                        };
                    }
                }
            }
        }
    }
}