using System.Text.Json.Serialization;

namespace Five9.Api.Models
{
    public class CallCenterEventModel
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("agentId")]
        public Guid AgentId { get; set; }

        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        [JsonPropertyName("timestampUtc")]
        public DateTime TimestampUtc { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("queueIds")]
        public List<Guid> QueueIds { get; set; }
    }
}
