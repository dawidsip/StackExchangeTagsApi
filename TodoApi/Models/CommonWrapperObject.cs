using System.Text.Json.Serialization;

namespace TodoApi.Models;
public class CommonWrapperObject<T>
{
    [JsonPropertyName("backoff")]
    public int? Backoff { get; set; }

    [JsonPropertyName("error_id")]
    public int? ErrorId { get; set; }

    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("error_name")]
    public string ErrorName { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("items")]
    public List<T> Items { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("quota_max")]
    public int QuotaMax { get; set; }

    [JsonPropertyName("quota_remaining")]
    public int QuotaRemaining { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
// backoffinteger
// may be absent
//  error_idinteger, refers to an error
// may be absent
//  error_messagestring
// may be absent
//  error_namestring
// may be absent
//  has_moreboolean
//  itemsan array of the type found in type
//  pageinteger
//  page_sizeinteger
//  quota_maxinteger
//  quota_remaininginteger
//  totalinteger
//  typestring