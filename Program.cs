using System;
using System.Threading.Tasks;
using GithubActivity.Application;
using GithubActivity.Network;

namespace GithubActivity;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 1)
            Console.WriteLine("Please provide a Github Username!");

        NetworkClass.GithubUsername = args[0];

        App app = new();
        await app.Run();
    }
}