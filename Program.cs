using System;
using System.Threading.Tasks;
using GithubActivity.Application;
using GithubActivity.Network;
using GithubActivity.Types;

namespace GithubActivity;

class Program
{
    public static void Instructions()
    {
        Console.WriteLine("Github Activity Tracker");
        Console.WriteLine("       ----------       ");
        Console.WriteLine("Input the github username as the first argument.");
        Console.WriteLine("As a second argument, input the event type you want to filter. [OPTIONAL]");
        Console.WriteLine("       ----------       ");
        Console.WriteLine("Event types that are supported:");
        Console.WriteLine($"- {EventTypes.PushEvent}.");
        Console.WriteLine($"- {EventTypes.IssuesEvent}.");
        Console.WriteLine($"- {EventTypes.WatchEvent}.");
        Console.WriteLine($"- {EventTypes.ForkEvent}.");
        Console.WriteLine($"- {EventTypes.CreateEvent}.");
        Console.WriteLine("       ----------       ");
    }

    static async Task Main(string[] args)
    {
        string firstArgument = args[0];
        
        switch(firstArgument)
        {
            case "h":
            case "-h":
            case "help":
            case "--help":
                Instructions();
            return;
        }

        if (args.Length < 1)
        {
            Console.WriteLine("Please provide a Github Username!");
            return;
        }

        GlobalData.GithubUsername = args[0];
        GlobalData.Filter = args[1];

        App app = new();
        await app.Run();
    }
}