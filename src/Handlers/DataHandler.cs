using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.EventHandler;
using GithubActivity.Interfaces;
using GithubActivity.Structs;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public List<string> parsedEvents = new();
    private Dictionary<string, IEventParser> EventHandlers = new();

    public DataHandler()
    {
        EventHandlers.Add("PushEvent", new PushEvent());
        EventHandlers.Add("WatchEvent", new WatchEvent());
    }

    public void ParseData(JsonNode jsonData)
    {
        PreviousEventData prevData = new();
        JsonArray jsonArray = jsonData.AsArray();

        foreach(var element in jsonArray)
        {
            if (element == null) continue;
            
            JsonNode? eventType = element["type"];
            if (eventType == null) continue;

            string eventTypeValue = eventType.ToString();

            if (EventHandlers.TryGetValue(eventTypeValue, out IEventParser? eventParser))
            {
                if (eventParser == null) continue;

                eventParser.ParseEvent(element, prevData, parsedEvents);

                prevData.previousEventType = eventTypeValue;
                prevData.previousRepository = element["repo"]?["name"]?.ToString();
            }
        }
    }

    public void PrintParsedData()
    {
        foreach(string data in parsedEvents)
        {
            Console.WriteLine(data);
        }
    }
}