using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class TextScanner : ITokenScanner
{
    private readonly SpecScanner specScanner = new();
    
    public Token? Scan(string markdown)
    {
        var textValue = markdown
                .Select(c => c.ToString())
                .TakeWhile(c => specScanner.Scan(c) != null)
                .ToString();
        return textValue == null ? null : new Token(TokenType.TEXT, textValue);
    }
}