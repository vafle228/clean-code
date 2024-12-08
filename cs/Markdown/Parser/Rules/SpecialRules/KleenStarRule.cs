using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.SpecialRules;

public class KleenStarRule(IParsingRule pattern) : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var nodes = tokens.KleenStarMatch(pattern, begin);
        var consumed = nodes.Aggregate(0, (acc, node) => acc + node.Consumed);
        return consumed == 0 ? null : new SpecNode(nodes, begin, consumed);
    }
}