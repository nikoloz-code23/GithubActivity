using GithubActivity.DataTypes;

namespace GithubActivity.Interfaces;

public interface IEventParser
{
    public string ParseEvent(EventData eventData);
}