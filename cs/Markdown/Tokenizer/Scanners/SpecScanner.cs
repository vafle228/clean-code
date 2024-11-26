using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class SpecScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0) 
        => Scan(markdown[begin], begin);

    public Token? Scan(char symbol, int begin = 0) => symbol switch
    {
        '_' => new Token(TokenType.UNDERSCORE, begin, 1),
        '\n' => new Token(TokenType.NEW_LINE, begin, 1),
        _ => null
    };
}