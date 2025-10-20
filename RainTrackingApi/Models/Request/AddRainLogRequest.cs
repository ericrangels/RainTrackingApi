using System.ComponentModel.DataAnnotations;

namespace RainTrackingApi.Models.Request
{
    public class AddRainLogRequest
    {
        [Required]
        public bool? Rain { get; set; }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }
    }
}
