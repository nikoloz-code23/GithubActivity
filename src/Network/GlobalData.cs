namespace GithubActivity.Network;

public static class GlobalData
{
    private static string githubUsername = string.Empty;
    public static string GithubUsername
    { 
        get { return githubUsername; } 
        set { githubUsername = value.Trim(); } 
    }
    
    private static string filter = string.Empty;
    public static string Filter 
    { 
        get { return filter; } 
        set { filter = value.ToLowerInvariant().Trim(); }
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