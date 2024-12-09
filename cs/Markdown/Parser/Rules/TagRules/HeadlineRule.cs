using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.SpecialRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class HeadlineRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var resultRule = new AndRule([
            new PatternRule([TokenType.HASH_TAG, TokenType.SPACE]), new ParagraphRule(),
        ]);
        return resultRule.Match(tokens, begin) is SpecNode node ? BuildNode(node) : null;
    }

    private static TagNode BuildNode(SpecNode specNode)
    {
        var valueNode = (specNode.Nodes.Second() as TagNode)!;
        return new TagNode(NodeType.HEADLINE, valueNode.Children, specNode.Start, specNode.Consumed);
    }
}