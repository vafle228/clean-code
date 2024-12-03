using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.BoolRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class BoldRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        return !InWordBoldRule.IsTagInWord(tokens, begin)
            ? MatchBold(tokens, begin)
            : new InWordBoldRule().Match(tokens, begin);
    }
    
    private static TagNode? MatchBold(List<Token> tokens, int begin = 0)
    {
        var valueRule = new OrRule(new ItalicRule(), new TextRule());
        var pattern = new AndRule([
            PatternRule.DoubleUnderscoreRule(),
            BorderRule.NoSpaceOnBorderRule(new KleenStarRule(valueRule)),
            PatternRule.DoubleUnderscoreRule()
        ]);
        var continuesRule = new OrRule(TokenType.NEW_LINE, TokenType.SPACE);
        
        var resultRule = new ContinuesRule(pattern, continuesRule);
        return resultRule.Match(tokens, begin) is SpecNode specNode ? BuildNode(specNode) : null;
    }

    private static TagNode? BuildNode(SpecNode node)
    {
        var valueNode = node.Children.Second() as SpecNode;
        return valueNode is null ? null : new TagNode(NodeType.BOLD, valueNode.Children, node.Consumed);
    }
}