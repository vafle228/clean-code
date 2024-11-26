﻿using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class SpecScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0)
    {
        var tokenType = GetTokenType(markdown[begin]);
        if (tokenType is null) return null;
        
        var notNullType = (TokenType)tokenType;
        return new Token(notNullType, begin, 1);
    }
    
    public static bool CanScan(char symbol) 
        => GetTokenType(symbol) != null;

    private static TokenType? GetTokenType(char symbol) => symbol switch
    {
        ' ' => TokenType.SPACE,
        '\n' => TokenType.NEW_LINE,
        '_' => TokenType.UNDERSCORE,
        _ => null
    };
}