using System.IO;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using GithubActivity.Network;

namespace GithubActivity.Utilities;

public static class DataUtility
{
    public async static Task<JsonNode?> GetData(string dataUrl)
    {
        JsonNode? jsonData = null;

        try
        {
            using Stream stream = await NetworkClass.client.GetStreamAsync(dataUrl);
            jsonData = await JsonNode.ParseAsync(stream);
        }
        catch
        {
            return null;
        }

        return jsonData;
    }
}