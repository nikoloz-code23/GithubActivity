using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using GithubActivity.Utilities;
using GithubActivity.Handlers;
using GithubActivity.Data;

namespace GithubActivity.Application;

public class App
{
    static readonly HttpClient client = new();
    
    public App()
    {
        // Setup all the necessary stuff for the HttpClient here.
        client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
    }

    public async Task Run()
    {
        DataHandler handler = new();

        JsonNode? jsonData = await DataUtility.GetData(client, GlobalData.GithubEventUrl());

        if (jsonData == null)
        {
            Console.WriteLine("The Github User doesn't exist or has no activity yet!");
            return;
        }

        await handler.ParseData(jsonData, client);

        handler.PrintParsedData();
    }
}