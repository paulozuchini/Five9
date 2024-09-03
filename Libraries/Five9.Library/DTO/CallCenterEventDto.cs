namespace Five9.Library.DTO
{
    public class CallCenterEventDto
    {
        public long Id { get; set; }

        public Guid AgentId { get; set; }

        public string AgentName { get; set; }

        public DateTime TimestampUtc { get; set; }

        public string Action {  get; set; }

        public List<Guid> QueueIds { get; set; }
    }
}
