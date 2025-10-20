using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RainTrackingApi.Controllers;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Models.Request;
using RainTrackingApi.Models.Response;
using RainTrackingApi.Services.Interfaces;
using Xunit;

namespace RainTrackingApi.Tests.Controllers
{
    public class RainLogControllerTests
    {
        private readonly Mock<IRainLogService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RainLogController _controller;

        public RainLogControllerTests()
        {
            _serviceMock = new Mock<IRainLogService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new RainLogController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PostRainLog_MapsAndReturnsCreated()
        {
            // Arrange
            var userId = "user-b";
            var requestDto = new AddRainLogRequest { Rain = true, Latitude = 1m, Longitude = 2m };
            var createModel = new CreateUserRainLogDto { Rain = true, Latitude = 1m, Longitude = 2m, UserIdentifier = userId };

            _mapperMock.Setup(m => m.Map<CreateUserRainLogDto>(requestDto)).Returns(createModel);

            var createdEntity = new UserRainLog { Id = 99, Rain = true };
            _serviceMock.Setup(s => s.CreateAsync(It.Is<CreateUserRainLogDto>(m => m.UserIdentifier == userId && m.Rain == requestDto.Rain)))
                .ReturnsAsync(createdEntity);

            var createdDto = new RainLogResponse { UserIdentifier = userId, Rain = true };
            _mapperMock.Setup(m => m.Map<RainLogResponse>(createdEntity)).Returns(createdDto);

            // Act
            var actionResult = await _controller.PostRainLog(userId, requestDto);
            var createdResult = actionResult as CreatedAtActionResult;

            // Assert
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(createdDto);

            _serviceMock.Verify(s => s.CreateAsync(It.IsAny<CreateUserRainLogDto>()), Times.Once);
        }
    }
}
