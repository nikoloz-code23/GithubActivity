using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    public string ParseEvent(GithubEventData githubEventData)
    {
        int commitAmount = githubEventData.PayloadData.CommitSize;

        if(commitAmount > 1)
        {
            return $"- Pushed {commitAmount} commits to {githubEventData.RepositoryName}";
        }
        return $"- Pushed a commit to {githubEventData.RepositoryName}";
    }
}