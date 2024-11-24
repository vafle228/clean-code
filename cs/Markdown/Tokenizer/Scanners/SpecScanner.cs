using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class SpecScanner : ITokenScanner
{
    public Token? Scan(string markdown) => markdown[0] switch
    {
        '_' => new Token(TokenType.UNDERSCORE, "_"),
        '\n' => new Token(TokenType.NEW_LINE, "\n"),
        _ => null
    };
}