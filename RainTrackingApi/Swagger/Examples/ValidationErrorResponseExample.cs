using RainTrackingApi.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace RainTrackingApi.Swagger.Examples
{
    public class ValidationErrorResponseExample : IExamplesProvider<ValidationErrorResponse>
    {
        public ValidationErrorResponse GetExamples()
        {
            return new ValidationErrorResponse
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Errors = new Dictionary<string, string[]>
                {
                    {
                        "x-userId", new[]
                        {
                            "The userIdentifier field is required.",
                            "The field must contain a non-empty, non-whitespace value."
                        }
                    }
                },
                TraceId = "00-aee3f44db62908e94ed36f931970eaa1-46313aea3590f79c-00"
            };
        }
    }
}