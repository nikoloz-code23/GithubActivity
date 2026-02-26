using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    // TODO: Think of efficient logic for shortening duplicates to a count view instead.
    public void ParseEvent(JsonNode jsonNode, List<string> result);
}