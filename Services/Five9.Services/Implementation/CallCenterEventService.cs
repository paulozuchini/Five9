using AutoMapper;
using Five9.Library.Data.Context;
using Five9.Library.DTO;
using Five9.Library.Entities;
using Five9.Services.Interfaces;

namespace Five9.Services.Implementation
{
    public class CallCenterEventService : ICallCenterEventService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CallCenterEventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public long Create(CallCenterEventDto dto)
        {
            var entity = _mapper.Map<CallCenterEventEntity>(dto);
            var addedEntity = _context.CallCenterEvents.Add(entity);
            _context.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public void Delete(long id)
        {
            var result = GetById(id);
            var entity = _mapper.Map<CallCenterEventEntity>(result);
            _context.CallCenterEvents.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<CallCenterEventDto> GetAll()
        {
            var dtoResult = new List<CallCenterEventDto>();
            var result = _context.CallCenterEvents;
            
            if(result != null)
                _mapper.Map(result, dtoResult);

            return dtoResult;
        }

        public CallCenterEventDto GetById(long id)
        {
            var result = _context.CallCenterEvents.Find(id);
            if (result == null) throw new KeyNotFoundException("Call Center Event not found");
            return _mapper.Map<CallCenterEventDto>(result);
        }

        public void Update(long id, CallCenterEventDto dto)
        {
            var result = GetById(id);
            var entity = _mapper.Map<CallCenterEventEntity>(result);
            _context.CallCenterEvents.Update(entity);
            _context.SaveChanges();
        }
    }
}
