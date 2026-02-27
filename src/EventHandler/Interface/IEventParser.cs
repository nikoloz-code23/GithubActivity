using System.Collections.Generic;
using GithubActivity.Data;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    public void ParseEvent(GithubEventData githubEventData, List<string> result);
}