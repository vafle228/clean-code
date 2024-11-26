using Markdown.Parser.MatchTools.Models;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public class MatchPattern
{
    public static PatternBody Start() => new();
    public static PatternBody StartWith(TokenType tokenType) => new(tokenType);

    public class PatternBody
    {
        private readonly List<TokenType> patternRaw = [];
        
        public PatternBody(TokenType tokenType) 
            => patternRaw.Add(tokenType);
        
        public PatternBody() { }
        
        # region fluent api methods
        public PatternBody ContinueWith(TokenType nextType)
        {
            patternRaw.Add(nextType);
            return this;
        }

        public PatternBody ContinueWith(List<TokenType> nextTypes)
        {
            patternRaw.AddRange(nextTypes);
            return this;
        }

        public PatternBody ContinueWithRepeat(List<TokenType> repeatedTypes, int count)
        {
            for (var _ = 0; _ < count; _++)
                ContinueWith(repeatedTypes);
            return this;
        }

        public Pattern EndWith(TokenType endType)
        {
            patternRaw.Add(endType);
            return End();
        }
        
        public Pattern End() => new(patternRaw);
        #endregion fluent api methods
    }
}