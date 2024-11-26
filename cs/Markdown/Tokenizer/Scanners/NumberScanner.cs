using Markdown.Tokenizer.Tokens;

namespace Markdown.Tokenizer.Scanners;

public class NumberScanner : ITokenScanner
{
    public Token? Scan(string markdown, int begin = 0)
    {
        var numberIterator = markdown
            .Skip(begin)
            .TakeWhile(CanScan);
        var numberLen = numberIterator.Count();
        return numberLen == 0 ? null : new Token(TokenType.NUMBER, begin, numberLen, markdown);
    }
    
    public static bool CanScan(char symbol) => char.IsDigit(symbol);
}