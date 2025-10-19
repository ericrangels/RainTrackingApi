using System.ComponentModel.DataAnnotations.Schema;

namespace RainTrackingApi.Models.Domain
{
    public class UserRainLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User User { get; set; }
        public bool Rain { get; set; }
        public DateTime Timestamp { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitude { get; set; }
    }
}
