using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.Structs;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    public void ParseEvent(JsonNode jsonNode, PreviousEventData prevData, List<string> result);
}