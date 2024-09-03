using System.Text.Json.Serialization;

namespace Five9.Api.Models
{
    public class AgentStateModel
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("agentId")]
        public Guid AgentId { get; set; }
        
        [JsonPropertyName("state")]
        public string AgentState { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}
