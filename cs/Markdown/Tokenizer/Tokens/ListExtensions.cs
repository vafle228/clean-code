﻿using System.Text;

namespace Markdown.Tokenizer.Tokens;

public static class ListExtensions
{
    public static string ToText(this List<Token> tokens) 
        => tokens.Aggregate(new StringBuilder(), (sb, t) => sb.Append(t.Value)).ToString();
}