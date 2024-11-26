using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class TextScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0)
    {
        var valueIterator = markdown
            .Skip(begin)
            .TakeWhile(CanScan);
        var valueLen = valueIterator.Count();
        return valueLen == 0 ? null : new Token(TokenType.TEXT, begin, valueLen, markdown);
    }

    private static bool CanScan(char symbol)
        => !SpecScanner.CanScan(symbol) && !NumberScanner.CanScan(symbol);
}