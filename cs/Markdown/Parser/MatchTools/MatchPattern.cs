using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public class MatchPattern
{
    public static PatternBody StartWith(TokenType tokenType) => new(tokenType);

    public class PatternBody(TokenType startType)
    {
        private readonly List<TokenType> pattern = [startType];

        public PatternBody ContinueWith(TokenType nextType)
        {
            pattern.Add(nextType);
            return this;
        }

        public List<TokenType> EndWith(TokenType endType)
        {
            pattern.Add(endType);
            return pattern;
        }
        
        public List<TokenType> End() => pattern;
    }
}