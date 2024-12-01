﻿namespace Markdown.Parser.Rules.Tools;

public static class ListOrderExtensions
{
    public static T? Second<T>(this List<T> list) 
        => list.Count < 2 ? default : list[1];
    
    public static T? Third<T>(this List<T> list)
        => list.Count < 3 ? default : list[2];
}