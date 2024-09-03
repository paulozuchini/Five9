namespace Five9.Library.Entities
{
    public class AgentStateEntity
    {
        public long Id { get; set; }
        public Guid AgentId { get; set; }
        public string State { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
