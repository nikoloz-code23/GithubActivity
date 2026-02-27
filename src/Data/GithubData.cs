using System.Text.Json.Nodes;

namespace GithubActivity.Data;

public class GithubEventData
{
    public JsonNode? CurrentEvent {get; set;}
    public string? PreviousEventType {get; set;}
    public string? PreviousRepository {get; set;}
    
    public string? GetRepoName()
    {
        return CurrentEvent?["repo"]?["name"]?.ToString().ToLower();   
    }

    public string? GetEventType()
    {
        return CurrentEvent?["type"]?.ToString().ToLower();
    }

    public void SetPreviousData()
    {
        PreviousEventType = GetEventType();
        PreviousRepository = GetRepoName();
    }
}