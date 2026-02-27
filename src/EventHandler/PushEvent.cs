using System;
using System.Collections.Generic;
using GithubActivity.Interfaces;
using GithubActivity.Data;
using GithubActivity.Enums;
using GithubActivity.ExtensionMethods;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    private static int amount = 1;
    public void ParseEvent(GithubEventData githubEventData, List<string> result)
    {
        string? repositoryName = githubEventData.GetRepoName();

        if (repositoryName == null)
            throw new Exception("Can't get repository name. JSON is wrong or non-existent. Aborting!");
        
        string eventType = EventTypes.PushEvent.Name();
        if (githubEventData.PreviousEventType != eventType || 
            repositoryName != githubEventData.PreviousRepository)
        {
            amount = 1;
            result.Add($"- Pushed a commit to {repositoryName}");
            return;
        }
        
        amount += 1;
        result[result.Count-1] = $"- Pushed {amount} commits to {repositoryName}";
    }
}