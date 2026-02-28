using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class CreateEvent : IEventParser
{
    public string ParseEvent(GithubEventData githubEventData)
    {
        string eventName = githubEventData.Payload["ref_type"]!.GetValue<string>();

        return $"- Created {eventName} in {githubEventData.RepositoryName}";
    }
}