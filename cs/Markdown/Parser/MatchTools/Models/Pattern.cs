﻿using System.Collections;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools.Models;

public class Pattern(List<TokenType> pattern) : IEnumerable<TokenType>
{
    public List<TokenType> Value { get; } = pattern;
    
    public int Count => Value.Count;

    public static Pattern Empty => new([]);
    public bool IsEmpty => Value.Count == 0;

    IEnumerator IEnumerable.GetEnumerator() => Value.GetEnumerator();
    public IEnumerator<TokenType> GetEnumerator() => Value.GetEnumerator();

    public override string ToString()
    {
        return string.Join(" ", Value.Select(tt => tt.ToString()));
    }
}