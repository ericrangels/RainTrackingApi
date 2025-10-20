namespace RainTrackingApi.Models.Response
{
    public class RainLogResponse
    {
        public DateTime Timestamp { get; set; }
        public bool Rain { get; set; }
        public string? UserIdentifier { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
