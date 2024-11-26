using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class TextScanner : ITokenScanner
{
    private readonly SpecScanner specScanner = new();
    
    public Token? Scan(string markdown, int begin = 0)
    {
        var textIterator = markdown
            .Skip(begin)
            .Select(c => c.ToString())
            .TakeWhile(c => specScanner.Scan(c) == null);
        var textValue = string.Join("", textIterator);
        return textValue == string.Empty ? null : new Token(TokenType.TEXT, textValue);
    }
}