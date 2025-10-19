using RainTrackingApi.Models.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace RainTrackingApi.Swagger.Examples
{
    public class AddRainLogRequestExample : IExamplesProvider<AddRainLogRequestDto>
    {
        public AddRainLogRequestDto GetExamples()
        {
            return new AddRainLogRequestDto
            {
                Rain = true,
                Latitude = 51.5074m,
                Longitude = -0.1278m
            };
        }
    }
}
