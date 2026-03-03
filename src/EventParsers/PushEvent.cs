using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    public CommitJsonData response { get; set; }
    public string ParseEvent(EventData eventData)
    {
        int commitAmount = response.CommitData.Count;

        if(commitAmount > 1)
        {
            return $"- Pushed {commitAmount} commits to {eventData.Repo.Name}";
        }
        return $"- Pushed a commit to {eventData.Repo.Name}";
    }
}