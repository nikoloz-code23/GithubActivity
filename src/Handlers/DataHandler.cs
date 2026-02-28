using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Nodes;

using GithubActivity.EventHandler;
using GithubActivity.Interfaces;
using GithubActivity.Data;
using GithubActivity.Utilities;
using GithubActivity.Enums;
using GithubActivity.ExtensionMethods;
using GithubActivity.Structs;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public List<GithubEventData> parsedData = new();
    private Dictionary<string, IEventParser> EventHandlers = new();
    private List<Task<GithubEventData>> tasks = new();

    public DataHandler()
    {
        EventHandlers.Add(EventTypes.PushEvent.Name(), new PushEvent());
        EventHandlers.Add(EventTypes.WatchEvent.Name(), new WatchEvent());
    }

    public async Task ParseData(JsonNode jsonData, HttpClient client)
    {
        JsonArray jsonArray = jsonData.AsArray();
        Console.WriteLine("Parsing...");

        foreach(JsonNode? element in jsonArray)
        {
            tasks.Add(Task.Run(async () =>
            {
                string? eventType = element!["type"]?.GetValue<string>();
                if (string.IsNullOrEmpty(eventType))
                    throw new Exception ("JSON Data is invalid. Aborting!");
                
                string? repoName = element["repo"]?["name"]?.GetValue<string>();

                Payload payload = new(
                    element["payload"]?["head"]?.GetValue<string>(),
                    element["payload"]?["before"]?.GetValue<string>()
                );

                string compareUrl = GlobalData.GithubCompareUrl(repoName, payload.Before, payload.Head);
                JsonNode? response = await DataUtility.GetData(client, compareUrl);
                
                if (response?["commits"] is JsonArray responseArray)
                {
                    payload.CommitSize = responseArray.Count;
                }

                return new GithubEventData(
                    eventType.ToLower().Trim(),
                    repoName,
                    payload
                );
            }));
        }

        GithubEventData[] results = await Task.WhenAll(tasks);
        parsedData.AddRange(results);
    }

    public void PrintParsedData()
    {
        foreach(GithubEventData data in parsedData)
        {
            if (EventHandlers.TryGetValue(data.EventType, out IEventParser? eventParser))
            {
                if (eventParser == null) continue;
                Console.WriteLine(eventParser.ParseEvent(data));
            }
        }
    }
}