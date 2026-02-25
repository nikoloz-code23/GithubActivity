using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace GithubActivity.Utilities;

public static class DataUtility
{
    public async static Task<JsonNode?> GetData(HttpClient client, string dataUrl)
    {
        JsonNode? jsonData = null;

        try
        {
            using Stream stream = await client.GetStreamAsync(dataUrl);
            jsonData = await JsonNode.ParseAsync(stream);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return jsonData;
    } 
}