using System.Text.Json.Serialization;

namespace Application.Services.VisualCrossing.Responses
{
    public record VcGeospatialWeatherResponse
    {
        [JsonPropertyName("days")]
        public List<Days> Days { get; set; } = new();
    }

    public record Days
    {
        [JsonPropertyName("hours")]
        public List<Hours> Hours { get; set; }
    }

    public record Hours
    {
        [JsonPropertyName("datetimeEpoch")]
        public long DatetimeEpoch { get; set; }
        [JsonPropertyName("cloudcover")]
        public double Cloudcover { get; set; }
    }
}
