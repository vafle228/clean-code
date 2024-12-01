using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.Tools;

public static class ListMatchExtensions
{
    public static List<Node> MatchPattern(this List<Token> tokens, List<IParsingRule> pattern, int begin = 0)
    {
        List<Node> nodes = [];
        
        foreach (var patternRule in pattern)
        {
            var node = patternRule.Match(tokens, begin);
            if (node is null) return [];
            nodes.Add(node); begin += node.Consumed; 
        }
        return nodes;
    }
    
    public static List<Node> KleenStarMatch(this List<Token> tokens, IParsingRule pattern, int begin = 0)
    {
        List<Node> nodes = [];
        while (true)
        {
            var node = pattern.Match(tokens, begin);
            if (node is null) return nodes;
            begin += node.Consumed; nodes.Add(node);
        }
    }

    public static Node? FirstMatch(this List<Token> tokens, List<IParsingRule> patterns, int begin = 0)
    {
        var match = patterns
            .Select(rule => rule.Match(tokens, begin))
            .FirstOrDefault(node => node is not null, null);
        return match;
    }
}