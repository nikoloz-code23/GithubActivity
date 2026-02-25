using System;
using System.Text.Json.Nodes;

namespace GithubActivity.Handlers;

public class DataHandler
{
    public void FilterData(JsonNode jsonData)
    {
        JsonArray jsonArray = jsonData.AsArray();

        foreach(var element in jsonArray)
        {
            if (element == null) continue;
            
            JsonNode? eventType = element["type"];
            if (eventType == null) continue;

            string eventTypeValue = eventType.ToString();

            if (eventTypeValue != "PushEvent") continue;

            // TODO: Separate class that will handle the events and the data with them.
            // Console.WriteLine(element["repo"]["name"]);
            Console.WriteLine(element);
            Console.WriteLine("---");
        }
        // TODO: Depending on the type, save important data in a Class and have a ToString function.
    }
}