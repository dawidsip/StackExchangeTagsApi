using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;
using Microsoft.EntityFrameworkCore;
using TodoApi.Interfaces;

namespace TodoApi.Tests
{
    public class TagsControllerTests
    {
        public TagsControllerTests()
        {
            Setup();
        }

        public Mock<ITodoContext> todoContextMock;
        public void Setup()
        {
            List<Tag> tagsData = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag1" },
                new Tag { Id = 2, Name = "Tag2" }
            };

            var mockDbSet = new Mock<DbSet<Tag>>();
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tagsData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tagsData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tagsData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(tagsData.AsQueryable().GetEnumerator());

            var mockTodoContext = new Mock<ITodoContext>();
            mockTodoContext.Setup(c => c.Tags).Returns(mockDbSet.Object);

            todoContextMock = mockTodoContext;
        }

        [Fact]
        public void GetTags_ReturnsOkResult()
        {
            // Arrange
            var loggerMock1 = new Mock<ILogger<HelperService>>();
            var loggerMock2 = new Mock<ILogger<TagsController>>();
            var helperServiceMock = new Mock<HelperService>(loggerMock1.Object, todoContextMock.Object);
            var controller = new TagsController(loggerMock2.Object, helperServiceMock.Object);

            // Act
            var result = controller.GetTags();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RepopulateTags_ReturnsOkResult()
        {
            // Arrange
            var loggerMock1 = new Mock<ILogger<HelperService>>();
            var loggerMock2 = new Mock<ILogger<TagsController>>();
            var helperServiceMock = new Mock<HelperService>(loggerMock1.Object, todoContextMock.Object);
            var controller = new TagsController(loggerMock2.Object, helperServiceMock.Object);

            // Act
            var result = controller.RepopulateTags();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}