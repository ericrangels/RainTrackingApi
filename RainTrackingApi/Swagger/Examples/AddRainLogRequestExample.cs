using RainTrackingApi.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace RainTrackingApi.Swagger.Examples
{
    public class AddRainLogRequestExample : IExamplesProvider<AddRainLogRequest>
    {
        public AddRainLogRequest GetExamples()
        {
            return new AddRainLogRequest
            {
                Rain = true,
                Latitude = 51.5074m,
                Longitude = -0.1278m
            };
        }
    }
}
