using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Five9.Api.Models
{
    public class CallCenterEventModel
    {
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        [JsonPropertyName("agentId")]
        public Guid AgentId { get; set; }

        [Required]
        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        [Required]
        [JsonPropertyName("timestampUtc")]
        public DateTime TimestampUtc { get; set; }

        [Required]
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("queueIds")]
        public List<Guid> QueueIds { get; set; }
    }
}
