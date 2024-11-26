namespace Markdown.Tokenizer.Tokens;

public class Token(TokenType tokenType, int begin, int length, string sourceText)
{
    private string? value;

    public int Begin { get; } = begin;
    public int Length { get; } = length;
    public TokenType TokenType { get; } = tokenType;

    public string GetValue()
    {
        return value ??= sourceText.Substring(Begin, Length);
    }

    public override string ToString()
    {
        return $"Token {TokenType} | Value \"{GetValue()}\"";
    }
}