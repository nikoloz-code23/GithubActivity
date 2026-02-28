using System.Text.Json.Nodes; 

using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    public JsonNode? response { get; set; }
    public string ParseEvent(GithubEventData githubEventData)
    {
        int commitAmount = 0;

        if (response?["commits"] is JsonArray responseArray)
        {
            commitAmount = responseArray.Count;
        }

        if(commitAmount > 1)
        {
            return $"- Pushed {commitAmount} commits to {githubEventData.RepositoryName}";
        }
        return $"- Pushed a commit to {githubEventData.RepositoryName}";
    }
}