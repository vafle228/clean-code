using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class SpecScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0) => markdown[begin] switch
    {
        '_' => new Token(TokenType.UNDERSCORE, "_"),
        '\n' => new Token(TokenType.NEW_LINE, "\n"),
        _ => null
    };
}