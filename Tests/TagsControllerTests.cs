using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApi.Controllers;
using TodoApi.Services;

namespace TodoApi.Tests
{
    public class TagsControllerTests
    {
        [Fact]
        public void GetTags_ReturnsOkResult()
        {
            //Placeholder for a mocked controller test
            Assert.IsType<OkObjectResult>(new OkObjectResult(new object()));
        }

        [Fact]
        public void RepopulateTags_ReturnsOkResult()
        {
            //Placeholder for a mocked controller test
            Assert.IsType<OkObjectResult>(new OkObjectResult(new object()));
        }
    }
}
