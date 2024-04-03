using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly HelperService _helperService;
    private readonly TodoContext _dbContext;

    public TagsController(ILogger<TagsController> logger, HelperService helperService, TodoApi.Services.TodoContext dbContext)
    {
        _logger = logger;
        _helperService = helperService;
        _dbContext = dbContext;    
    }

    /// <summary>
    /// Retrieves a paginated list of tags.
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 1000)</param>
    /// <param name="orderBy">Property to order by (default: name), valid values are 'name' or 'share'</param>
    /// <param name="orderDirection">Order direction (default: asc), valid values are 'asc' or 'desc'</param>
    /// <returns>A paginated list of tags</returns>
    [HttpGet(Name = "Tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetTags(int page = 1, int pageSize = 1000, string orderBy = "name", string orderDirection = "asc")
    {
        page = Math.Max(page, 1);
        pageSize = Math.Min(Math.Max(pageSize, 1), 1000);

        var propertyInfo = typeof(Tag).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
        {   
            _logger.LogWarning("Invalid orderBy parameter. Valid values are 'name' or 'share'.");
            return BadRequest("Invalid orderBy parameter. Valid values are 'name' or 'share'.");
        }

        var tagsQuery = _dbContext.Tags.AsQueryable();
        var parameter = Expression.Parameter(typeof(Tag), "tag");
        var property = Expression.Property(parameter, propertyInfo);
        var selector = Expression.Lambda(property, parameter);

        MethodCallExpression orderByExpression = Expression.Call(
            typeof(Queryable),
            orderDirection.ToLower() == "asc" ? "OrderBy" : "OrderByDescending",
            new Type[] { typeof(Tag), propertyInfo.PropertyType },
            tagsQuery.Expression,
            Expression.Quote(selector));

        tagsQuery = tagsQuery.Provider.CreateQuery<Tag>(orderByExpression);

        var tags = tagsQuery.Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

        _logger.LogInformation("Retrieved {Count} tags from the database.", tags.Count);
        return Ok(tags);
    }

    /// <summary>
    /// Forces the application to repopulate the Tags table from StackOverflow.
    /// </summary>
    [HttpPost("repopulate")]
    public IActionResult RepopulateTags()
    {
        try
        {
            _helperService.PopulateTags();
            _logger.LogInformation("Tags table has been repopulated successfully.");
            return Ok("Tags table has been repopulated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to repopulate Tags table.");
            return StatusCode(500, "An error occurred while repopulating Tags table.");
        }
    }
}