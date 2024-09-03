using AutoMapper;
using Five9.Api.Models;
using Five9.Library.DTO;
using Five9.Library.Exceptions;
using Five9.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Five9.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventProcessingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IEventProcessingService _service;

        public EventProcessingController(
            ILogger logger,
            IMapper mapper,
            IEventProcessingService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public IActionResult RegisterNewEvent(CallCenterEventModel model)
        {
            try
            {
                var dto = _mapper.Map<CallCenterEventDto>(model);

                var processedEvent = _service.ProcessNewEvent(dto);

                return Ok(new { message = "Event processed" });
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(LateEventException))
                {
                    return BadRequest($@"Event registration not accepted. 
                        The event timestamp ({model.TimestampUtc}) should not be greater than one hour ago.");
                }

                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
