using Five9.Library.DTO;

namespace Five9.Services.Interfaces
{
    public interface IAgentStateService
    {
        IEnumerable<AgentStateDto> GetAll();

        AgentStateDto GetById(long id);

        long Create(AgentStateDto dto);

        void Update(long id, AgentStateDto dto);

        void Delete(long id);
    }
}
