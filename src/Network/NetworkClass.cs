using System;
using System.Net.Http;

namespace GithubActivity.Network;

public static class NetworkClass
{
    public static readonly HttpClient client;
    
    static NetworkClass()
    {
        SocketsHttpHandler handler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };

        client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
    }
}