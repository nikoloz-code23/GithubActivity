using System;
using GithubActivity.Interfaces;
using GithubActivity.Data;
using GithubActivity.Utilities;

namespace GithubActivity.EventHandler;

public class IssuesEvent : IEventParser
{
    public string ParseEvent(EventData eventData)
    {
        string payloadAction = eventData.Payload.EventAction.ReturnCapitalized();

        return $"- {payloadAction} an issue in {eventData.Repo.Name}";
    }
}