using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Linq;

using GithubActivity.Data;
using GithubActivity.Types;
using GithubActivity.Network;
using GithubActivity.EventHandler;
using GithubActivity.Utilities;
using GithubActivity.Interfaces;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public List<GithubEventData> parsedData = new();

    public void ParseToGithubEventData(JsonNode jsonData)
    {
        JsonArray jsonArray = jsonData.AsArray();
        Console.WriteLine("Parsing...");

        foreach(JsonNode? element in jsonArray)
        {
            string? eventType = element!["type"]?.GetValue<string>();
            if (string.IsNullOrEmpty(eventType))
                throw new Exception ("JSON Data is invalid. Aborting!");
            
            string? repoName = element["repo"]?["name"]?.GetValue<string>();
            parsedData.Add(
                new GithubEventData(
                    eventType.ToLower().Trim(),
                    repoName,
                    element["payload"]!
                )
            );
        }
    }

    public void PrintParsedData(IEnumerable<string> taskResults)
    {
        foreach(string data in taskResults)
        {
            if (string.IsNullOrEmpty(data) || data == string.Empty)
                continue;
            Console.WriteLine(data);
        }      
    }

    public async Task<string> RunEventParsers(GithubEventData data)
    {
        IEventParser eventParser;
        switch (data.EventType)
        {
            case EventTypes.PushEvent:
                string beforeCommit = data.Payload["before"]!.GetValue<string>();
                string headCommit = data.Payload["head"]!.GetValue<string>();

                string compareUrl = NetworkClass.GithubCompareUrl(data.RepositoryName, beforeCommit, headCommit);
                PushEvent pushEvent = new()
                {
                    response = await DataUtility.GetData(compareUrl)
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

    public async Task<IEnumerable<string>> ReturnParsedData()
    {
        List<Task<string>> tasks = [];
        
        foreach(GithubEventData data in parsedData)
        {
            tasks.Add(RunEventParsers(data));
        }

        IEnumerable<string> result = await Task.WhenAll(tasks);
        return result.Where(element => element != string.Empty);
    }

}