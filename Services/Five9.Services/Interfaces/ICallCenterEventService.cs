using Five9.Library.DTO;

namespace Five9.Services.Interfaces
{
    public interface ICallCenterEventService
    {
        IEnumerable<CallCenterEventDto> GetAll();
        
        CallCenterEventDto GetById(long id);
        
        long Create(CallCenterEventDto dto);
        
        void Update (long id, CallCenterEventDto dto);

        void Delete (long id);
    }
}
