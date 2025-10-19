using RainTrackingApi.Models.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace RainTrackingApi.Swagger.Examples
{
    public class RainLogResponseExample : IExamplesProvider<RainLogResponseDto>
    {
        public RainLogResponseDto GetExamples()
        {
            return new RainLogResponseDto
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
