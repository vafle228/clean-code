using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public class MatchPattern
{
    public static PatternBody Start() => new();
    public static PatternBody StartWith(TokenType tokenType) => new(tokenType);

    public class PatternBody
    {
        private readonly List<TokenType> pattern = [];
        
        public PatternBody(TokenType tokenType) 
            => pattern.Add(tokenType);
        
        public PatternBody() { }
        
        # region fluent api methods
        public PatternBody ContinueWith(TokenType nextType)
        {
            pattern.Add(nextType);
            return this;
        }

        public PatternBody ContinueWith(List<TokenType> nextTypes)
        {
            pattern.AddRange(nextTypes);
            return this;
        }

        public PatternBody ContinueWithRepeat(List<TokenType> repeatedTypes, int count)
        {
            for (var _ = 0; _ < count; _++)
                pattern.AddRange(repeatedTypes);
            return this;
        }

        public List<TokenType> EndWith(TokenType endType)
        {
            pattern.Add(endType);
            return pattern;
        }
        
        public List<TokenType> End() => pattern;
        #endregion fluent api methods
    }
}