using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.BoolRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class ParagraphRule : IParsingRule
{
    private static readonly OrRule SentenceValues = new([
        new ItalicRule(), new BoldRule(), new TextRule(), 
        PatternRule.DoubleUnderscoreRule(), 
        new OrRule(TokenType.NUMBER, TokenType.UNDERSCORE)
    ]);

    private readonly List<IParsingRule> pattern =
    [
        new KleenStarRule(SentenceValues),
        new PatternRule(TokenType.NEW_LINE),
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.First() is not SpecNode specNode) return null;

        return new TagNode(NodeType.PARAGRAPH, specNode.Children, specNode.Consumed + 1);
    }
}