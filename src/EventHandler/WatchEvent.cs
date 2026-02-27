using System;
using System.Collections.Generic;
using GithubActivity.Interfaces;
using GithubActivity.Data;

namespace GithubActivity.EventHandler;

public class WatchEvent : IEventParser
{
    public void ParseEvent(GithubEventData githubEventData, List<string> result)
    {
        string? repositoryName = githubEventData.GetRepoName();

        if (repositoryName == null)
            throw new Exception("Can't get repository name. JSON is wrong or non-existent. Aborting!");
        
        result.Add($"- Is watching an event in {repositoryName}");
    }
}