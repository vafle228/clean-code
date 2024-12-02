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
        PatternRule.DoubleUnderscoreRule(),
        new KleenStarRule(new OrRule(new ItalicRule(), new TextRule())),
        PatternRule.DoubleUnderscoreRule(),
    ];

    private readonly OrRule continuesRule = new(TokenType.NEW_LINE, TokenType.SPACE);

    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var innerRule = new InWordBoldRule();
        if (begin != 0 && tokens[begin - 1].TokenType == TokenType.WORD)
            return innerRule.Match(tokens, begin);
        return innerRule.Match(tokens, begin) ?? MatchBold(tokens, begin);
    }
    
    private TagNode? MatchBold(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not SpecNode specNode) return null;
        
        var resultNode = BuildNode(specNode);
        
        var endWithWord = EndWithWordOrItalic(specNode.Children.Last());
        var startWithWord = StartWithWordOrItalic(specNode.Children.First());
        var hasRightContinues = HasRightContinues(tokens, begin + resultNode.Consumed);

        return endWithWord && startWithWord && hasRightContinues ? resultNode : null;
    }
    
    private bool HasRightContinues(List<Token> tokens, int begin)
    {
        if (tokens.Count == begin) return true;
        return continuesRule.Match(tokens, begin) is not null;
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
        => new(NodeType.BOLD, specNode.Children, specNode.Consumed + 4);
}