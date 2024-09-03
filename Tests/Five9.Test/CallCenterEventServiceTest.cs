using Five9.Library.DTO;
using Five9.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace Five9.Test
{
    public class CallCenterEventServiceTest
    {
        [Fact]
        public async void ServiceShouldGetAll()
        {
            var mockService = new Mock<ICallCenterEventService>();
            var mockResult = new List<CallCenterEventDto>()
            {
                new CallCenterEventDto()
                {
                    Id = 1,
                    AgentId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                    AgentName = "John Smith",
                    TimestampUtc = DateTime.UtcNow,
                    Action = "CALL_STARTED",
                    QueueIds = new List<Guid>()
                    {
                        new Guid("3a8cc33a-3f09-4ce5-9c53-e94585a410c8"),
                        new Guid("3d887de3-8351-4391-b155-e174f472456a")
                    }
                }
            };

            mockService.Setup(s => s.GetAll()).Returns(mockResult);

            var result = mockService.Object.GetAll();

            result.Should().BeOfType<List<CallCenterEventDto>>()
                .Which.FirstOrDefault()
                .AgentId.Should().Be(new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"));
        }

        //TODO ADD MORE TESTS LATER.
    }
}