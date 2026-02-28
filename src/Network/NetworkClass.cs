using System;
using System.Net.Http;

namespace GithubActivity.Network;

public class NetworkClass
{
    public static readonly HttpClient client;
    public static string GithubUsername { get; set; } = "";
    
    static NetworkClass()
    {
        SocketsHttpHandler handler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };

        client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
    }

    public static string GithubEventUrl()
    {
        return $"https://api.github.com/users/{GithubUsername}/events";
    }

    public static string GithubCompareUrl(string? repoName, string? before, string? after)
    {
        return $"https://api.github.com/repos/{repoName}/compare/{before}...{after}";
    }
}