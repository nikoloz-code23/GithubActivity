using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class ForkEvent : IEventParser
{
    public string ParseEvent(EventData eventData)
    {
        return $"- Forked {eventData.Repo.Name}";
    }
}