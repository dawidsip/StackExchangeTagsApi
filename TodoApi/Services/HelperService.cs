using TodoApi.Models;
using System.Net;
using System.Text.Json;

namespace TodoApi.Services;

public class HelperService
{
    private readonly ILogger<HelperService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public HelperService(ILogger<HelperService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async void PopulateTags()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
            dbContext.Tags.RemoveRange(dbContext.Tags);
            dbContext.SaveChanges();
            
            var tagsToSave = await GetTagsAsync();
            dbContext.Tags.AddRange(tagsToSave);                
            var count = dbContext.SaveChanges();

            _logger.LogInformation("Tags in DataBase: {Count}", count);
        }
    }

    private async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        using (HttpClient client = new HttpClient(handler))
        {
            var tags = await FetchTags(client);
            int totalCount = tags.Sum(tag => tag.Count);
            tags = CalculateShares(tags, totalCount);
            _logger.LogInformation("Fetched and then added 'Share' property to this many tags: " + tags.Count());
            return tags;
        }
    }

    private IEnumerable<Tag> CalculateShares(IEnumerable<Tag> tags, int totalCount) =>
        tags.Select(tag => {tag.Share = Math.Round((double)tag.Count / totalCount * 100, 2); return tag;});

    private async Task<IEnumerable<Tag>> FetchTags(HttpClient client, int minCount = 1000)
    {
        var tags = new List<Tag>();
        for(int i = 1; tags.Count < minCount; i++)
            tags = tags.Concat(await FetchTagsPage(client, i)).ToList();

        return tags;
    }

    private async Task<IEnumerable<Tag>> FetchTagsPage(HttpClient client, int pageNumber = 1, int pageSize = 100)
    {
        HttpResponseMessage response = await client.GetAsync($"https://api.stackexchange.com/2.3/tags?order=desc&page={pageNumber}&pagesize={pageSize}&sort=activity&site=stackoverflow");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
                    
            // System.Console.WriteLine(responseBody);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CommonWrapperObject<Tag> apiResponse = null;
            try
            {
                apiResponse = JsonSerializer.Deserialize<CommonWrapperObject<Tag>>(responseBody, options);     
            }
            catch (JsonException ex)
            {
                _logger.LogError("Failed to deserialize the API response. Error: " + ex.Message);
            }
            var tagsArray = apiResponse?.Items.ToArray();
            _logger.LogInformation("Tags received: " + tagsArray.Length);

            return tagsArray is null ? new Tag[]{new Tag(){HasSynonyms = false, IsModeratorOnly = false, IsRequired = false, Count = 0, Name = "Error"}} : tagsArray;
        }
        else
        {
            _logger.LogWarning("Failed to make the API call. Status code: " + response.StatusCode);
            return new Tag[]{new Tag(){HasSynonyms = false, IsModeratorOnly = false, IsRequired = false, Count = 0, Name = "Error"}};
        }
    }    
}