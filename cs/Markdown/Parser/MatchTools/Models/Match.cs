namespace Markdown.Parser.MatchTools.Models;

public class Match(int start, int length)
{
    public int Start { get; } = start;
    public int Length { get; } = length;
    public int End { get; } = start + length;
    
    public bool IsEmpty => Start == -1 && Length == -1;
    public static Match ZeroMatch => new(-1, -1);
}