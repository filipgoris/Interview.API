using ABB.Interview.Domain;
using System.Text.Json.Serialization;

namespace ABB.Interview.Data.Dto
{
    internal class DeviceReadingDto
    {
        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; } = string.Empty;

        [JsonPropertyName("deviceName")]
        public string DeviceName { get; set; } = string.Empty;

        [JsonPropertyName("deviceGroup")]
        public string DeviceGroup { get; set; } = string.Empty;

        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;

        [JsonPropertyName("power")]
        public Power[]? Power { get; set; }
    }
}
