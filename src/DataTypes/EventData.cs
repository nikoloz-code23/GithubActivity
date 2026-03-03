using System.Text.Json.Serialization;

namespace GithubActivity.DataTypes;

public record EventData
{
    private string eventType = string.Empty;
    [JsonPropertyName("type")]
    public string Type { 
        get { return eventType; } 
        set { eventType = value.ToLowerInvariant(); } 
    }

    [JsonPropertyName("repo")]
    public RepoData Repo { get; set; }

    [JsonPropertyName("payload")]
    public PayloadData Payload { get; set; }

    public override string ToString()
    {
        return $"{Type}, {Repo.Name}";
    }
}

public struct RepoData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public struct PayloadData
{
    [JsonPropertyName("head")]
    public string Head { get; set; }

    [JsonPropertyName("before")]
    public string Before { get; set; }

    [JsonPropertyName("action")]
    public string EventAction { get; set; }

    [JsonPropertyName("ref_type")]
    public string RefType { get; set; }
}