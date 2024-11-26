using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer;

public class MarkdownTokenizer
{
    private readonly ITokenScanner[] scanners = [
        new SpecScanner(), new TextScanner()
    ];

    public List<Token> Tokenize(string markdown)
    {
        var begin = 0;
        var tokenList = new List<Token>();
        
        while (begin < markdown.Length)
        {
            var token = scanners
                .Select(sc => sc.Scan(markdown, begin))
                .First(token => token is not null);
            begin += token!.Length; tokenList.Add(token);
        }
        return tokenList;
    }
}