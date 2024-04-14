using TodoApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class TagsControllerIntegrationTests
    {
        private readonly TagsController _controller;

        public TagsControllerIntegrationTests()
        {
            
            var mockLogger = new Mock<ILogger<TagsController>>();
            var mockHelperService = new Mock<HelperService>();
            var mockDbContext = new Mock<TodoContext>();
            _controller = new TagsController(mockLogger.Object, mockHelperService.Object);
        }
    }
}
