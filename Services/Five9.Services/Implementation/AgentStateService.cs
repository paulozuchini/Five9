using AutoMapper;
using Five9.Library.Data.Context;
using Five9.Library.DTO;
using Five9.Library.Entities;
using Five9.Services.Interfaces;

namespace Five9.Services.Implementation
{
    public class AgentStateService : IAgentStateService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AgentStateService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public long Create(AgentStateDto dto)
        {
            var entity = _mapper.Map<AgentStateEntity>(dto);
            var addedEntity = _context.AgentStates.Add(entity);
            _context.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public void Delete(long id)
        {
            var result = GetById(id);
            var entity = _mapper.Map<AgentStateEntity>(result);
            _context.AgentStates.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<AgentStateDto> GetAll()
        {
            var dtoResult = new List<AgentStateDto>();
            var result = _context.AgentStates;

            if (result != null)
                _mapper.Map(result, dtoResult);

            return dtoResult;
        }

        public AgentStateDto GetById(long id)
        {
            var result = _context.AgentStates.Find(id);
            if (result == null) throw new KeyNotFoundException("Call Center Event not found");
            return _mapper.Map<AgentStateDto>(result);
        }

        public void Update(long id, AgentStateDto dto)
        {
            var result = GetById(id);
            var entity = _mapper.Map<AgentStateEntity>(result);
            _context.AgentStates.Update(entity);
            _context.SaveChanges();
        }
    }
}