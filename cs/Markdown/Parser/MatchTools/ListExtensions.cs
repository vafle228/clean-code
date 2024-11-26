using Markdown.Parser.MatchTools.Models;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools;

public static class ListExtensions
{
    public static Match SingleMatch(this List<Token> tokens, Pattern pattern, int begin = 0)
    {
        if (tokens.Count - begin < pattern.Count) 
            return Match.ZeroMatch;
        
        if (pattern.Count == 0) return Match.ZeroMatch;

        var isMatched = tokens
            .Skip(begin).Take(pattern.Count).Zip(pattern)
            .All(pair => pair.First.TokenType == pair.Second);
        return isMatched ? new Match(begin, pattern.Count) : Match.ZeroMatch;
    }

    public static List<Match> KleenStarMatch(this List<Token> tokens, Pattern pattern, int begin = 0)
    {
        List<Match> matches = [];
        while (true)
        {
            var match = tokens.SingleMatch(pattern, begin);
            if (match.IsEmpty) return matches;
            begin += match.Length; matches.Add(match);
        }
    }

    public static Match FirstSingleMatch(this List<Token> tokens, List<Pattern> patterns, int begin = 0)
    {
        var match = patterns
            .Select(pattern => tokens.SingleMatch(pattern, begin))
            .FirstOrDefault(match => !match.IsEmpty, Match.ZeroMatch);
        return match;
    }
}