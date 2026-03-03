using System.Threading.Tasks;
using System.Net.Http.Json;

using GithubActivity.Data;
using GithubActivity.Types;
using GithubActivity.Network;
using GithubActivity.EventHandler;
using GithubActivity.Interfaces;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public async Task<string> RunEventParsers(EventData data)
    {
        IEventParser eventParser;
        switch (data.Type)
        {
            case EventTypes.PushEvent:
                string compareUrl = NetworkClass.GithubCompareUrl(data.Repo.Name, data.Payload.Before, data.Payload.Head);
                PushEvent pushEvent = new()
                {
                    response = await NetworkClass.GetAndSerializeJsonData<CommitJsonData>(compareUrl)
                };
                eventParser = pushEvent;
            break;

            case EventTypes.IssuesEvent:
                eventParser = new IssuesEvent();
            break;

            case EventTypes.WatchEvent:
                eventParser = new WatchEvent();
            break;

            case EventTypes.ForkEvent:
                eventParser = new ForkEvent();
            break;

            default:
                return string.Empty;
        }
        return eventParser.ParseEvent(data);
    }
}