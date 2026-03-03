using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using GithubActivity.Data;
using GithubActivity.Network;
using GithubActivity.Handlers;

namespace GithubActivity.Application;

using EnumerableEventData = IEnumerable<EventData>;

public class App
{
    public EnumerableEventData? eventData;
    private DataHandler handler = new();

    public async Task Run()
    {
        eventData = await NetworkClass.GetAndSerializeJsonData<EnumerableEventData>(NetworkClass.GithubEventUrl());

        if (eventData == null)
            throw new Exception("Something went wrong. Aborting!");

        IEnumerable<Task<string>> tasks = eventData.Select(async eventElement =>
        {
            string text = await handler.RunEventParsers(eventElement);
            return text;
        });
        
        string[] output = await Task.WhenAll(tasks);
        Array.ForEach(await Task.WhenAll(tasks), (text) =>
        {
            if(text != string.Empty)
                Console.WriteLine(text);
        });
    }
}