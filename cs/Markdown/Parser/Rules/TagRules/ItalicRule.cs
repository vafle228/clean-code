using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.SpecialRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class ItalicRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        return !InWordItalicRule.IsTagInWord(tokens, begin)
            ? MatchItalic(tokens, begin)
            : new InWordItalicRule().Match(tokens, begin);
    }

    private static TagNode? MatchItalic(List<Token> tokens, int begin)
    {
        var pattern = new AndRule([
            new PatternRule(TokenType.UNDERSCORE),
            new ConditionalRule(new TextRule(), HasRightBorders),
            new PatternRule(TokenType.UNDERSCORE),
        ]);
        var continuesRule = new OrRule([
            PatternRule.DoubleUnderscoreRule(),
            new PatternRule(TokenType.NEW_LINE),
            new PatternRule(TokenType.SPACE),
        ]);
        
        var resultRule = new ContinuesRule(pattern, continuesRule);
        return resultRule.Match(tokens, begin) is SpecNode specNode ? BuildNode(specNode) : null;
    }

    private static TagNode BuildNode(SpecNode node) 
        => new(NodeType.ITALIC, node.Children.Second()!, node.Consumed);

    private static bool HasRightBorders(Node node)
        => node is TextNode textNode
           && textNode.Last.TokenType != TokenType.SPACE
           && textNode.First.TokenType != TokenType.SPACE;
}