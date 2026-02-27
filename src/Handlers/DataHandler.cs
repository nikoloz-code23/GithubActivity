using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.EventHandler;
using GithubActivity.Interfaces;
using GithubActivity.Data;
using GithubActivity.Enums;
using GithubActivity.ExtensionMethods;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public List<string> parsedEvents = new();
    private Dictionary<string, IEventParser> EventHandlers = new();

    public DataHandler()
    {
        EventHandlers.Add(EventTypes.PushEvent.Name(), new PushEvent());
        EventHandlers.Add(EventTypes.WatchEvent.Name(), new WatchEvent());
    }

    public void ParseData(JsonNode jsonData)
    {
        JsonArray jsonArray = jsonData.AsArray();
        GithubEventData githubEventData = new();

        foreach(var element in jsonArray)
        {
            if (element == null) continue;
            githubEventData.CurrentEvent = element;

            string? eventTypeValue = githubEventData.GetEventType();
            if (eventTypeValue == null) continue;

            if (EventHandlers.TryGetValue(eventTypeValue, out IEventParser? eventParser))
            {
                if (eventParser == null) continue;
                eventParser.ParseEvent(githubEventData, parsedEvents);
            }

            githubEventData.SetPreviousData();
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