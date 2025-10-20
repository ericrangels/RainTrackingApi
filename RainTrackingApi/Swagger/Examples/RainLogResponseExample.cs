using RainTrackingApi.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace RainTrackingApi.Swagger.Examples
{
    public class RainLogResponseExample : IExamplesProvider<RainLogResponse>
    {
        public RainLogResponse GetExamples()
        {
            return new RainLogResponse
            {
                Rain = true,
                Timestamp = DateTime.UtcNow,
                UserIdentifier = "user-123",
                Latitude = 51.5074m,
                Longitude = -0.1278m
            };
        }
    }
}
