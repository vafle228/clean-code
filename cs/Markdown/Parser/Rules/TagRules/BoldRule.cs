using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.BoolRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class BoldRule : IParsingRule
{
    private readonly List<IParsingRule> pattern =
    [
        new PatternRule([TokenType.UNDERSCORE, TokenType.UNDERSCORE]),
        new KleenStarRule(new OrRule(new ItalicRule(), new TextRule())),
        new PatternRule([TokenType.UNDERSCORE, TokenType.UNDERSCORE]),
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not SpecNode specNode) return null;
        
        var endWithWord = EndWithWordOrItalic(specNode.Children.Last());
        var startWithWord = StartWithWordOrItalic(specNode.Children.First());

        return endWithWord && startWithWord ? BuildNode(specNode) : null;
    }

    private static bool StartWithWordOrItalic(Node node)
    {
        if (node.NodeType == NodeType.ITALIC) return true;
        return node is TextNode { First.TokenType: TokenType.WORD };
    }

    private static bool EndWithWordOrItalic(Node node)
    {
        if (node.NodeType == NodeType.ITALIC) return true;
        return node is TextNode { Last.TokenType: TokenType.WORD };
    }

    private static TagNode BuildNode(SpecNode specNode) 
        => new(NodeType.BOLD, specNode.Children, specNode.Consumed + 2);
}