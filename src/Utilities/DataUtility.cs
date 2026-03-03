using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using GithubActivity.Network;

namespace GithubActivity.Utilities;

public static class DataUtility
{
    public static async Task<T?> GetAndSerializeJsonData<T>(string uri)
    {
        try
        {    
            HttpResponseMessage response = await NetworkClass.client.GetAsync(uri);
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
}