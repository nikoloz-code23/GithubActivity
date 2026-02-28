using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class WatchEvent : IEventParser
{
    public string ParseEvent(GithubEventData githubEventData)
    {
        return $"- Is watching an event in {githubEventData.RepositoryName}";
    }
}