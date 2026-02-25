using System.Threading.Tasks;
using GithubActivity.Application;

namespace GithubActivity;

class Program
{
    // TODO: Make sure that the args is used to specify the GitHub User.
    static async Task Main(string[] args)
    {
        App app = new();
        
        await app.Run();
    }
}