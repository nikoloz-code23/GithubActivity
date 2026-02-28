using GithubActivity.Data;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    public string ParseEvent(GithubEventData githubEventData);
}