namespace RainTrackingApi.Models.Response
{
    public class ValidationErrorResponse
    {
        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Status { get; set; }

        public Dictionary<string, string[]> Errors { get; set; } = new();

        public string TraceId { get; set; } = string.Empty;
    }
}