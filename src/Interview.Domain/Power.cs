using System.Text.Json.Serialization;

namespace ABB.Interview.Domain
{
    public class Power
    {
        [JsonPropertyName("min")]
        public double Min {  get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }
        
        [JsonPropertyName("avg")]
        public double Avg { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
