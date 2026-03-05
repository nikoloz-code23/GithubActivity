using System.Threading.Tasks;

using GithubActivity.DataTypes;
using GithubActivity.Types;
using GithubActivity.Network;
using GithubActivity.Utilities;
using GithubActivity.Interfaces;
using GithubActivity.EventHandler;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public async Task<string> RunEventParsers(EventData data)
    {
        IEventParser eventParser;
        switch (data.Type)
        {
            case EventTypes.PushEvent:
                string compareUrl = GlobalData.GithubCompareUrl(data.Repo.Name, data.Payload.Before, data.Payload.Head);
                PushEvent pushEvent = new()
                {
                    response = await DataUtility.GetAndSerializeJsonData<CommitJsonData>(compareUrl)
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