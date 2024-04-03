using System.Text.Json.Serialization;

namespace TodoApi.Models;
public class Tag
{
    public int Id { get; set; }
    
    [JsonPropertyName("has_synonyms")]
    public bool HasSynonyms { get; set; }

    [JsonPropertyName("is_moderator_only")]
    public bool IsModeratorOnly { get; set; }

    [JsonPropertyName("is_required")]
    public bool IsRequired { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public double Share { get; set; } = 0.0d;
}