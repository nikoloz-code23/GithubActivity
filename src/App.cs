using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using GithubActivity.Utilities;
using GithubActivity.Handlers;
using GithubActivity.Network;

namespace GithubActivity.Application;

public class App
{
    public async Task Run()
    {
        DataHandler handler = new();

        JsonNode? jsonData = await DataUtility.GetData(NetworkClass.GithubEventUrl());

        if (jsonData == null)
        {
            Console.WriteLine("The Github User doesn't exist or has no activity yet!");
            return;
        }

        handler.ParseToGithubEventData(jsonData);

        IEnumerable<string> results = await handler.ReturnParsedData();

        handler.PrintParsedData(results);
    }
}