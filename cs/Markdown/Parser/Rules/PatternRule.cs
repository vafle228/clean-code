using Markdown.Parser.MatchTools.Models;
using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules;

public class PatternRule(List<TokenType> pattern) : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        if (pattern.Count == 0) return null;
        if (tokens.Count - begin < pattern.Count) return null;
        
        var isMatched = tokens
            .Skip(begin).Take(pattern.Count).Zip(pattern)
            .All(pair => pair.First.TokenType == pair.Second);
        
        if (!isMatched) return null;
        var match = new TokenMatch(begin, pattern.Count, tokens);

        return new Node(NodeType.TEXT, match, pattern.Count);
    }
}