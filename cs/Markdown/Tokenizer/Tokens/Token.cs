namespace Markdown.Tokenizer.Tokens;

public class Token(TokenType tokenType, string value)
{
    public string Value { get; } = value;
    public TokenType TokenType { get; } = tokenType;
}