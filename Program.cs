using System.Threading.Tasks;
using GithubActivity.Application;
using GithubActivity.Data;

namespace GithubActivity;

class Program
{
    static async Task Main(string[] args)
    {
        GlobalData.GithubUsername = args[0];

        App app = new();
        await app.Run();
    }
}