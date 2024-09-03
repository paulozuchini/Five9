using Five9.Library.DTO;

namespace Five9.Services.Interfaces
{
    public interface IEventProcessingService
    {
        Task<AgentStateDto> ProcessNewEvent(CallCenterEventDto callCenterEvent);
    }
}
