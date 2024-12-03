using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class HeadlineRule : IParsingRule
{
    private readonly List<IParsingRule> pattern =
    [
        new PatternRule([TokenType.HASH_TAG, TokenType.SPACE]),
        new ParagraphRule(),
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not SpecNode specNode) return null;

        return new TagNode(NodeType.HEADLINE, specNode.Children, specNode.Consumed + 2);
    }
}