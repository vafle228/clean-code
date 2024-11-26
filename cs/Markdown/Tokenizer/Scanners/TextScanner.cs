using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class TextScanner : ITokenScanner
{
    private readonly SpecScanner specScanner = new();
    
    public Token? Scan(string markdown, int begin = 0)
    {
        var valueIterator = markdown
            .Skip(begin)
            .TakeWhile(c => specScanner.Scan(c) == null);
        var valueLen = valueIterator.Count();
        return valueLen == 0 ? null : new Token(TokenType.TEXT, begin, valueLen);
    }
}