namespace Markdown.Tokenizer.Tokens;

public class Token(TokenType tokenType, int begin, int length)
{
    private string? value = null;

    public int Begin { get; } = begin;
    public int Length { get; } = length;
    public TokenType TokenType { get; } = tokenType;

    public string GetValue(string text)
    {
        return value ??= text.Substring(Begin, Length);
    }
}