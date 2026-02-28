namespace GithubActivity.Data;

public static class GlobalData
{
    public static string GithubUsername { get; set; } = "";
    
    public static string GithubEventUrl()
    {
        return $"https://api.github.com/users/{GithubUsername}/events";
    }

    public static string GithubCompareUrl(string? repoName, string? before, string? after)
    {
        return $"https://api.github.com/repos/{repoName}/compare/{before}...{after}";
    }
}