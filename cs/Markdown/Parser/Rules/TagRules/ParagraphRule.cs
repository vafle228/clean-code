using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.SpecialRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class ParagraphRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var tagRules = new OrRule([
            new ItalicRule(), new BoldRule(), new TextRule(),
        ]);
        var tokenRules = new OrRule([
            PatternRule.DoubleUnderscoreRule(),
            new PatternRule(TokenType.NUMBER),
            new PatternRule(TokenType.UNDERSCORE)
        ]);
        
        var resultRule = new AndRule([
            new KleenStarRule(new OrRule(tagRules, tokenRules)),
            new PatternRule(TokenType.NEW_LINE)
        ]);
        return resultRule.Match(tokens, begin) is SpecNode node ? BuildNode(node) : null;
    }

    private static TagNode BuildNode(SpecNode node)
    {
        var valueNode = (node.Children.First() as SpecNode)!;
        return new TagNode(NodeType.PARAGRAPH, valueNode.Children, node.Consumed); 
    }
}