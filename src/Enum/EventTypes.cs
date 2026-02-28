namespace GithubActivity.Types;

public record EventTypes
{
    public const string 
        PushEvent = "pushevent",
        IssuesEvent = "issuesevent",
        WatchEvent = "watchevent",
        ForkEvent = "forkevent",
        CreateEvent = "createvent";
}