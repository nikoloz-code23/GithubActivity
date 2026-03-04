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
            using HttpResponseMessage response = await NetworkClass.client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                string statusCode = response.StatusCode.ToString();
                throw new Exception($"Couldn't get information from GitHub. Status code: {statusCode}");
            }
            string? responseString = await response.Content.ReadAsStringAsync();

            if (responseString == null)
                throw new Exception($"Couldn't get response content.");
            
            return JsonSerializer.Deserialize<T>(responseString);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Request has been cancelled. Closing...");
            Environment.Exit(1);
            return default;  
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Cannot connect to the server, please check your internet connection. Aborting!");
            Environment.Exit(1);
            return default;
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error: {e.Message}.\nClosing...");
            Environment.Exit(1);
            return default;
        }
    }
}