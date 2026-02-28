using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class ForkEvent : IEventParser
{
    public string ParseEvent(GithubEventData githubEventData)
    {
        return $"- Forked {githubEventData.RepositoryName}";
    }
}