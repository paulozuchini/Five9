using AutoMapper;
using Five9.Library.Data.Context;
using Five9.Library.DTO;
using Five9.Library.Exceptions;
using Five9.Services.Interfaces;

namespace Five9.Services.Implementation
{
    public class EventProcessingService : IEventProcessingService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICallCenterEventService _callCenterEventService;
        private readonly IAgentStateService _agentStateService;
        private const string AVAILABLE = "AVAILABLE";

        public EventProcessingService(
            ApplicationDbContext context,
            IMapper mapper,
            ICallCenterEventService callCenterEventService,
            IAgentStateService agentStateService)
        {
            _context = context;
            _mapper = mapper;
            _callCenterEventService = callCenterEventService;
            _agentStateService = agentStateService;
        }

        public async Task<AgentStateDto> ProcessNewEvent(CallCenterEventDto callCenterEvent)
        {
            // check if timestamp is over 1 hour late
            if ((DateTime.UtcNow.AddHours(-1) - callCenterEvent.TimestampUtc).TotalHours > 1)
            {
                throw new LateEventException();
            }

            // first, log the event in the database;
            _callCenterEventService.Create(callCenterEvent);

            // preppare agent state response
            var response = new AgentStateDto()
            {
                AgentId = callCenterEvent.AgentId,
                TimeStamp = callCenterEvent.TimestampUtc,
                State = AVAILABLE
            };

            // check the event activity
            switch (callCenterEvent.Action)
            {
                case "START_DO_NOT_DISTURB":
                    if(callCenterEvent.TimestampUtc.TimeOfDay > new TimeSpan(11, 00, 00)
                        && callCenterEvent.TimestampUtc.TimeOfDay < new TimeSpan(13, 00, 00))
                    {
                        response.State = "ON_LUNCH";
                    }
                    else
                    {
                        response.State = "DO_NOT_DISTURB";
                    }
                break;

                case "CALL_STARTED":
                    response.State = "ON_CALL";
                    break;

                default:
                    response.State = AVAILABLE;
                    break;
            }

            // save the agent state into the database
            _agentStateService.Create(response);

            return await Task.FromResult(response);
        }
    }
}