using GithubActivity.Types;

namespace GithubActivity.ExtensionMethods;

public static class ExtensionMethods
{
    public static string Name(this EventTypes eventType) =>
        eventType.ToString().ToLower();
}