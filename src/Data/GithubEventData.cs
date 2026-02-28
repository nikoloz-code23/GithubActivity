using System.Text.Json.Nodes;

namespace GithubActivity.Data;

public class GithubEventData
{
    public string EventType { get; set; }
    public string? RepositoryName { get; set; }
    public JsonNode Payload {get; set;}

    public GithubEventData(string eventType, string? repoName, JsonNode payloadData)
    {
        EventType = eventType;
        RepositoryName = repoName;
        Payload = payloadData;
    }
}