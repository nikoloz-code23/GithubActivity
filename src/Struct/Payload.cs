namespace GithubActivity.Structs;

public struct Payload(string? head, string? before, int commitSize = 0)
{
    public string? Head { get; set; } = head;
    public string? Before { get; set; } = before;
    public int CommitSize { get; set; } = commitSize;
}