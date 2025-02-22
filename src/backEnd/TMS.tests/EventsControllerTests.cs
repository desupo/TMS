using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TMS.application.DTOs;
using TMS.host.Commands;
using TMS.host.Controllers;

namespace TMS.tests;

public class EventsControllerTests
{
    [Fact]
    public async Task UploadEventsAsync_ValidFile_ReturnsOk()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new EventsController(mediatorMock.Object);

        var fileMock = new Mock<IFormFile>();
        var content = new MemoryStream(new byte[100]); // Simulating a file with content
        var fileName = "test.csv";

        fileMock.Setup(f => f.OpenReadStream()).Returns(content);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(content.Length);

        var request = new UploadEventRequest { File = fileMock.Object };

        // Act
        var result = await controller.UploadEventsAsync(request);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        mediatorMock.Verify(m => m.Send(It.IsAny<ProcessEquipmentEventsCommand>(), default), Times.Once);
    }
}