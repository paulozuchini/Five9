namespace Five9.Library.DTO
{
    public class AgentStateDto
    {
        public long Id { get; set; }
        public Guid AgentId { get; set; }
        public string State { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
