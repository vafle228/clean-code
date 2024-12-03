using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.BoolRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class ItalicRule : IParsingRule
{
    // private readonly List<IParsingRule> pattern =
    // [
    //     new PatternRule(TokenType.UNDERSCORE),
    //     new TextRule(),
    //     new PatternRule(TokenType.UNDERSCORE),
    // ];

    private readonly OrRule continuesRule = new(
        PatternRule.DoubleUnderscoreRule(),
        new OrRule(TokenType.NEW_LINE, TokenType.SPACE)
    );
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        return !InWordItalicRule.IsTagInWord(tokens, begin)
            ? MatchItalic(tokens, begin)
            : new InWordItalicRule().Match(tokens, begin);
    }

    private TagNode? MatchItalic(List<Token> tokens, int begin)
    {
        List<IParsingRule> pattern =
        [
            new PatternRule(TokenType.UNDERSCORE),
            new TextRule(),
            new PatternRule(TokenType.UNDERSCORE),
        ];
        
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not TextNode textNode) return null;
        
        var resultNode = BuildNode(textNode);
        
        var endWithNonSpace = textNode.Last.TokenType != TokenType.SPACE;
        var startWithNonSpace = textNode.First.TokenType != TokenType.SPACE;
        var hasRightContinues = HasRightContinues(tokens, begin + resultNode.Consumed);
        
        return hasRightContinues && endWithNonSpace && startWithNonSpace ? resultNode : null;
    }

    private bool HasRightContinues(List<Token> tokens, int begin)
    {
        if (tokens.Count == begin) return true;
        return continuesRule.Match(tokens, begin) is not null;
    }
    
    private static TagNode BuildNode(TextNode textNode) 
        => new(NodeType.ITALIC, textNode, textNode.Consumed + 2);
}