using System;

namespace GithubActivity.Utilities;

public static class StringUtility {
    public static string ReturnCapitalized(this string str)
    {
        return string.Concat(str[0].ToString().ToUpper(), str.AsSpan(1));
    }
}