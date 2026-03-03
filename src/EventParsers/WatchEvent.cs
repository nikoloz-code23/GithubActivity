using GithubActivity.Interfaces;
using GithubActivity.DataTypes;

namespace GithubActivity.EventHandler;

public class WatchEvent : IEventParser
{
    public string ParseEvent(EventData eventData)
    {
        return $"- Is watching an event in {eventData.Repo.Name}";
    }
}