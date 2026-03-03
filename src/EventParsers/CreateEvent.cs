using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class CreateEvent : IEventParser
{
    public string ParseEvent(EventData eventData)
    {
        string eventName = eventData.Payload.RefType;

        return $"- Created {eventName} in {eventData.Repo.Name}";
    }
}