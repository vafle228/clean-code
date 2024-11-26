using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public static class ListExtensions
{
    public static Match SingleMatch(this List<Token> tokens, List<TokenType> pattern, int begin = 0)
    {
        if (tokens.Count - begin < pattern.Count) 
            return Match.ZeroMatch;
        
        if (pattern.Count == 0) return Match.ZeroMatch;

        var isMatched = tokens
            .Skip(begin).Take(pattern.Count).Zip(pattern)
            .All(pair => pair.First.TokenType == pair.Second);
        return isMatched ? new Match(begin, pattern.Count) : Match.ZeroMatch;
    }
}