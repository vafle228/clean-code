using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer;

public class Tokenizer
{
    private ITokenScanner[] scanners = [new SpecScanner(), new TextScanner()];

    public List<Token> Tokenize(string markdown)
    {
        throw new NotImplementedException();
    }
}