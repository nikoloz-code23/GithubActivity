using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using GithubActivity.Data;

namespace GithubActivity.Network;

public static class NetworkClass
{
    public static readonly HttpClient client;
    
    private static string githubUsername = string.Empty;
    public static string GithubUsername { get { return githubUsername; } set { githubUsername = value.Trim(); } }
    
    static NetworkClass()
    {
        SocketsHttpHandler handler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };

        client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
    }

    public static async Task<T?> GetAndSerializeJsonData<T>(string uri)
    {
        try
        {    
            HttpResponseMessage response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode.ToString();
                throw new Exception($"Couldn't get information from GitHub. Status code: {statusCode}");
            }
            using Stream stringData = response.Content.ReadAsStream();
            return await JsonSerializer.DeserializeAsync<T>(stringData);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Request has been cancelled. Closing...");
            return default;
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Cannot connect to the server, please check your internet connection. Aborting!");
            return default;
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error: {e.Message}.\nClosing...");
            return default;
        }
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