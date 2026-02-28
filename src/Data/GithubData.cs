using GithubActivity.Structs;

namespace GithubActivity.Data;

public class GithubEventData
{
    public string EventType { get; set; } = "";
    public string? RepositoryName { get; set; }
    public Payload PayloadData { get; set; }

    public GithubEventData(string eventType, string? repoName, Payload payload)
    {
        EventType = eventType;
        RepositoryName = repoName;
        PayloadData = payload;
    }
}