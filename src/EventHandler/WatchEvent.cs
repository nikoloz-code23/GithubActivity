using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.Interfaces;
using GithubActivity.Structs;

namespace GithubActivity.EventHandler;

public class WatchEvent : IEventParser
{
    protected static PreviousEventData prevData;
    public void ParseEvent(JsonNode jsonNode, List<string> result)
    {
        string? repositoryName = jsonNode["repo"]?["name"]?.ToString();

        if (repositoryName == null)
            throw new Exception("Can't get repository name. JSON is wrong or non-existent. Aborting!");
        
        result.Add($"- Is watching an event in {repositoryName}");

        prevData.previousEventType = jsonNode["type"]!.ToString();
        prevData.previousRepository = repositoryName;
    }
}