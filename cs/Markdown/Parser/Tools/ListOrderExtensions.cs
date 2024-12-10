namespace Markdown.Parser.Tools;

public static class ListOrderExtensions
{
    public static T? Second<T>(this List<T> list) 
        => list.Count < 2 ? default : list[1];
}