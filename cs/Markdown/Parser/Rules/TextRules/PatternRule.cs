﻿using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TextRules;

public class PatternRule(List<TokenType> pattern) : IParsingRule
{
    public PatternRule(TokenType tokenType) 
        : this([tokenType]) 
    { }
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        if (pattern.Count == 0) return null;
        if (tokens.Count - begin < pattern.Count) return null;
        
        var isMatched = tokens
            .Skip(begin).Take(pattern.Count).Zip(pattern)
            .All(pair => pair.First.TokenType == pair.Second);
        return !isMatched ? null : new TextNode(begin, pattern.Count);
    }

    public static PatternRule DoubleUnderscoreRule()
        => new([TokenType.UNDERSCORE, TokenType.UNDERSCORE]);
}