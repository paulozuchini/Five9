using AutoMapper;
using Five9.Api.Models;
using Five9.Library.DTO;
using Five9.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Five9.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallCenterEventApiController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICallCenterEventService _service;

        public CallCenterEventApiController(
            ILogger logger,
            IMapper mapper,
            ICallCenterEventService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPost]
        public IActionResult Create(CallCenterEventModel model)
        {
            try
            {
                var dto = _mapper.Map<CallCenterEventDto>(model);
                _service.Create(dto);
                return Ok(new { message = "Event created" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CallCenterEventModel model)
        {
            try
            {
                var dto = _mapper.Map<CallCenterEventDto>(model);
                _service.Update(id, dto);
                return Ok(new { message = "Event updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { message = "Event deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
