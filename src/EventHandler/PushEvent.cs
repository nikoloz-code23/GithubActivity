using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.Interfaces;

namespace GithubActivity.EventHandler;

public class PushEvent : IEventParser
{
    public void ParseEvent(JsonNode jsonNode, List<string> result)
    {
        string? repositoryName = jsonNode["repo"]?["name"]?.ToString();
        if (repositoryName == null)
            throw new Exception("Can't get repository name. JSON is wrong or non-existent. Aborting!");
        
        result.Add($"- Pushed a commits to {repositoryName}");
    }
}