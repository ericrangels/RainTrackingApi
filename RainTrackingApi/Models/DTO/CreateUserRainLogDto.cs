using System.ComponentModel.DataAnnotations.Schema;

namespace RainTrackingApi.Models.DTO
{
    public class CreateUserRainLogDto
    {        
        public bool Rain { get; set; }
        public string? UserIdentifier { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
