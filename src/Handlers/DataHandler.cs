using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.EventHandler;
using GithubActivity.Interfaces;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public List<string> parsedEvents = new();
    private Dictionary<string, IEventParser> EventHandlers = new();

    public DataHandler()
    {
        EventHandlers.Add("PushEvent", new PushEvent());
    }

    public void ParseData(JsonNode jsonData)
    {
        JsonArray jsonArray = jsonData.AsArray();

        foreach(var element in jsonArray)
        {
            if (element == null) continue;
            
            JsonNode? eventType = element["type"];
            if (eventType == null) continue;

            string eventTypeValue = eventType.ToString();

            if (EventHandlers.TryGetValue(eventTypeValue, out IEventParser? value))
            {
                if (value == null) continue;
                value.ParseEvent(element, parsedEvents);
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