namespace Markdown.Parser.MatchTools;

public class Match(int start, int length)
{
    public int Start { get; } = start;
    public int Length { get; } = length;
    public int End { get; } = start + length;

    public static Match ZeroMatch { get; } = new(-1, -1);
    
    public bool IsEmpty => this == ZeroMatch;
}