using System.ComponentModel.DataAnnotations.Schema;

namespace RainTrackingApi.Models.Domain
{
    public class CreateUserRainLogModel
    {        
        public bool Rain { get; set; }
        public string? UserIdentifier { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
