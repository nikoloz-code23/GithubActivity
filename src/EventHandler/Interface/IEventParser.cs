using System.Collections.Generic;
using System.Text.Json.Nodes;
using GithubActivity.Structs;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    // TODO: Think of efficient logic for shortening duplicates to a count view instead.
    protected static PreviousEventData prevData;
    public void ParseEvent(JsonNode jsonNode, List<string> result);
}