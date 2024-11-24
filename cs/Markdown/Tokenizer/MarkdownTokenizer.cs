using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer;

public class MarkdownTokenizer
{
    private ITokenScanner[] scanners = [new SpecScanner(), new TextScanner()];

    public List<Token> Tokenize(string markdown)
    {
        throw new NotImplementedException();
    }
}