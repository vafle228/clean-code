using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public interface ITokenScanner
{
    public Token? Scan(string markdown);
}