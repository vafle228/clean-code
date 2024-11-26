using Markdown.Parser.MatchTools.Models;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public static class ListExtensions
{
    public static Match<Token> SingleMatch(this List<Token> tokens, Pattern pattern, int begin = 0)
    {
        if (tokens.Count - begin < pattern.Count) 
            return Match<Token>.ZeroMatch;
        
        if (pattern.Count == 0) return Match<Token>.ZeroMatch;

        var isMatched = tokens
            .Skip(begin).Take(pattern.Count).Zip(pattern)
            .All(pair => pair.First.TokenType == pair.Second);
        return isMatched ? new Match<Token>(begin, pattern.Count, tokens) : Match<Token>.ZeroMatch;
    }

    public static List<Match<Token>> KleenStarMatch(this List<Token> tokens, Pattern pattern, int begin = 0)
    {
        List<Match<Token>> matches = [];
        while (true)
        {
            var match = tokens.SingleMatch(pattern, begin);
            if (match.IsEmpty) return matches;
            begin += match.Length; matches.Add(match);
        }
    }

    public static Match<Token> FirstSingleMatch(this List<Token> tokens, List<Pattern> patterns, int begin = 0)
    {
        var match = patterns
            .Select(pattern => tokens.SingleMatch(pattern, begin))
            .FirstOrDefault(match => !match.IsEmpty, Match<Token>.ZeroMatch);
        return match;
    }

    public static List<Match<Token>> FirstKleenStarMatch(this List<Token> tokens, List<Pattern> patterns, int begin = 0)
    {
        var match = patterns
            .Select(pattern => tokens.KleenStarMatch(pattern, begin))
            .FirstOrDefault(matchList => matchList.Count != 0, []);
        return match;
    }
}