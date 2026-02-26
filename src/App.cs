using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using GithubActivity.Utilities;
using GithubActivity.Handlers;

namespace GithubActivity.Application;

public class App
{
    // Easy way to make sure it's a singleton.
    // Only one App at a time anyway, am i rite?
    static readonly HttpClient client = new();
    public string GithubUsername { get; set; } = "kamranahmedse";
    public string GithubURL { get; set; }
    
    public App()
    {
        // Setup all the necessary stuff for the HttpClient here.
        client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
        GithubURL = $"https://api.github.com/users/{GithubUsername}/events";
    }

    public async Task Run()
    {
        DataHandler handler = new();

        JsonNode? jsonData = await DataUtility.GetData(client, GithubURL);

        if (jsonData == null)
        {
            Console.WriteLine("The Github User doesn't exist or has no activity yet!");
            return;
        }

        handler.ParseData(jsonData);

        handler.PrintParsedData();
    }
}