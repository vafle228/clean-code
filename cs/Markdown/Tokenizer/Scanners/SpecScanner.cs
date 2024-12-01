using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class SpecScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0)
    {
        var firstCharType = GetTokenType(markdown[begin]);
        if (begin == markdown.Length - 1)
        {
            return BuildToken(firstCharType, markdown, begin);
        }
        var secondCharType = GetTokenType(markdown[begin + 1]);

        if (firstCharType == TokenType.UNDERSCORE && secondCharType == TokenType.UNDERSCORE)
        {
            return BuildToken(TokenType.DOUBLE_UNDERSCORE, markdown, begin);
        }
        return BuildToken(firstCharType, markdown, begin);
    }
    
    public static bool CanScan(char symbol) 
        => GetTokenType(symbol) != null;

    private static Token? BuildToken(TokenType? tokenType, string markdown, int begin)
    {
        if (tokenType is null) return null;
        
        var notNullType = (TokenType)tokenType;
        var length = tokenType == TokenType.DOUBLE_UNDERSCORE ? 2 : 1;
        
        return new Token(notNullType, begin, length, markdown);
    }

    private static TokenType? GetTokenType(char symbol) => symbol switch
    {
        ' ' => TokenType.SPACE,
        '\n' => TokenType.NEW_LINE,
        '_' => TokenType.UNDERSCORE,
        _ => null
    };
}