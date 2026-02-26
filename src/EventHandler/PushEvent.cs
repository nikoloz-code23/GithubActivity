using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.Interfaces;
using GithubActivity.Structs;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    private static int amount = 1;
    public void ParseEvent(JsonNode jsonNode, PreviousEventData prevData, List<string> result)
    {
        string? repositoryName = jsonNode["repo"]?["name"]?.ToString();

        if (repositoryName == null)
            throw new Exception("Can't get repository name. JSON is wrong or non-existent. Aborting!");
        
        if (prevData.previousEventType == "PushEvent" && repositoryName == prevData.previousRepository)
        {
            amount += 1;
            result[result.Count-1] = $"- Pushed {amount} commits to {repositoryName}";
        }
        else
        {
            amount = 1;
            result.Add($"- Pushed a commit to {repositoryName}");
        }
    }
}