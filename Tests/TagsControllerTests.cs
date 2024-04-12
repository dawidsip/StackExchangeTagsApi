using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Tests
{
    public class TagsControllerTests
    {
        // [Fact]
        // public void GetTags_ReturnsOkResult()
        // {
        //     //Placeholder for a mocked controller test
        //     Assert.IsType<OkObjectResult>(new OkObjectResult(new object()));
        // }

        // [Fact]
        // public void RepopulateTags_ReturnsOkResult()
        // {
        //     //Placeholder for a mocked controller test
        //     Assert.IsType<OkObjectResult>(new OkObjectResult(new object()));
        // }

        [Fact]
        public void GetTags_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TagsController>>();
            var helperServiceMock = new Mock<HelperService>();
            var dbContextMock = new Mock<TodoContext>();
            var controller = new TagsController(loggerMock.Object, helperServiceMock.Object, dbContextMock.Object);

            // Act
            var result = controller.GetTags();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RepopulateTags_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TagsController>>();
            var helperServiceMock = new Mock<HelperService>();
            var dbContextMock = new Mock<TodoContext>();
            var controller = new TagsController(loggerMock.Object, helperServiceMock.Object, dbContextMock.Object);

            // Act
            var result = controller.RepopulateTags();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
