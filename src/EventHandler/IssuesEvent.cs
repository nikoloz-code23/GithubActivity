using GithubActivity.Interfaces;
using GithubActivity.Data;
using System;

namespace GithubActivity.EventHandler;

public class IssuesEvent : IEventParser
{
    public string ParseEvent(GithubEventData githubEventData)
    {
        string payloadAction = githubEventData.Payload["action"]!.GetValue<string>();
        payloadAction = string.Concat(payloadAction[0].ToString().ToUpper(), payloadAction.AsSpan(1));

        return $"- {payloadAction} an issue in {githubEventData.RepositoryName}";
    }
}