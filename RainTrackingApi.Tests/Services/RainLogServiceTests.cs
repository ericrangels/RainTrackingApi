using AutoMapper;
using FluentAssertions;
using Moq;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Repositories.Interfaces;
using RainTrackingApi.Services;
using Xunit;

namespace RainTrackingApi.Tests.Services
{
    public class RainLogServiceTests
    {
        private readonly Mock<IRainTrackingRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<Microsoft.Extensions.Logging.ILogger<RainLogService>> _loggerMock;
        private readonly RainLogService _service;

        public RainLogServiceTests()
        {
            _repoMock = new Mock<IRainTrackingRepository>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<RainLogService>>();
            _service = new RainLogService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetByUserIdAsync_DelegatesToRepositoryWithFilter()
        {
            // Arrange
            var userId = "user-x";
            var logs = new List<UserRainLog>
        {
            new UserRainLog { Id = 1, Rain = true },
            new UserRainLog { Id = 2, Rain = false }
        };

            _repoMock.Setup(r => r.GetRainLogsByUserIdentifierAsync(userId, true))
                .ReturnsAsync(new List<UserRainLog> { logs[0] });

            // Act
            var result = await _service.GetByUserIdAsync(userId, true);

            // Assert
            result.Should().HaveCount(1);
            result[0].Id.Should().Be(1);
            _repoMock.Verify(r => r.GetRainLogsByUserIdentifierAsync(userId, true), Times.Once);
        }
    }
}
