using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GithubActivity.Data;

public struct CommitJsonData
{
    [JsonPropertyName("commits")]
    public List<CommitData> CommitData { get; set; }
}

public record CommitData
{
    [JsonPropertyName("sha")]
    public string Sha { get; set; } = string.Empty;
}