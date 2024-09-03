using Five9.Library.DTO;
using Five9.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace Five9.Test
{
    public class AgentStateServiceTest
    {
        [Fact]
        public async void ServiceShouldGetAll()
        {
            var mockService = new Mock<IAgentStateService>();
            var mockResult = new List<AgentStateDto>()
            {
                new AgentStateDto()
                {
                    Id = 1,
                    AgentId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                    State = "AVAILABLE"
                }
            };

            mockService.Setup(s => s.GetAll()).Returns(mockResult);

            var result = mockService.Object.GetAll();

            result.Should().BeOfType<List<AgentStateDto>>()
                .Which.FirstOrDefault()
                .AgentId.Should().Be(new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"));
        }

        //TODO ADD MORE TESTS LATER.
    }
}