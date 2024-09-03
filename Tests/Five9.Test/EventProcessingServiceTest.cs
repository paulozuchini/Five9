using Five9.Library.DTO;
using Five9.Library.Exceptions;
using Five9.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace Five9.Test
{
    public class EventProcessingServiceTest
    {
        [Fact]
        public async void ShouldThrowLateEventException()
        {
            var mockService = new Mock<IEventProcessingService>();
            var mockInput = new CallCenterEventDto()
            {
                Id = 1,
                AgentId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                AgentName = "John Smith",
                TimestampUtc = DateTime.UtcNow.AddHours(-2),
                Action = "CALL_STARTED",
                QueueIds = new List<Guid>()
                {
                    new Guid("3a8cc33a-3f09-4ce5-9c53-e94585a410c8"),
                    new Guid("3d887de3-8351-4391-b155-e174f472456a")
                }
            };

            mockService.Setup(s => s.ProcessNewEvent(mockInput))
                .Throws(new LateEventException());

            Func<Task> act = () => mockService.Object.ProcessNewEvent(mockInput);

            await act.Should().ThrowAsync<LateEventException>();
        }

        [Fact]
        public async void EventCallStartedShouldReturnOnCall()
        {
            var mockService = new Mock<IEventProcessingService>();
            var mockInput = new CallCenterEventDto()
            {
                Id = 1,
                AgentId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                AgentName = "John Smith",
                TimestampUtc = DateTime.UtcNow.AddHours(-2),
                Action = "CALL_STARTED",
                QueueIds = new List<Guid>()
                {
                    new Guid("3a8cc33a-3f09-4ce5-9c53-e94585a410c8"),
                    new Guid("3d887de3-8351-4391-b155-e174f472456a")
                }
            };

            var mockResult = new AgentStateDto()
            {
                Id = 1,
                AgentId = mockInput.AgentId,
                State = "ON_CALL",
                TimeStamp = mockInput.TimestampUtc
            };

            mockService.Setup(s => s.ProcessNewEvent(mockInput))
                .Returns(Task.FromResult(mockResult));

            var act = await mockService.Object.ProcessNewEvent(mockInput);

            act.Should().BeSameAs(mockResult);
        }

        [Fact]
        public async void AnyDifferentEventShouldReturnAvailable()
        {
            var mockService = new Mock<IEventProcessingService>();
            var mockInput = new CallCenterEventDto()
            {
                Id = 1,
                AgentId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                AgentName = "John Smith",
                TimestampUtc = DateTime.UtcNow.AddHours(-2),
                Action = "ANYTHING",
                QueueIds = new List<Guid>()
                {
                    new Guid("3a8cc33a-3f09-4ce5-9c53-e94585a410c8"),
                    new Guid("3d887de3-8351-4391-b155-e174f472456a")
                }
            };

            var mockResult = new AgentStateDto()
            {
                Id = 1,
                AgentId = mockInput.AgentId,
                State = "AVAILABLE",
                TimeStamp = mockInput.TimestampUtc
            };

            mockService.Setup(s => s.ProcessNewEvent(mockInput))
                .Returns(Task.FromResult(mockResult));

            var act = await mockService.Object.ProcessNewEvent(mockInput);

            act.Should().BeSameAs(mockResult);
        }
    }
}