using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.BoolRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class InWordItalicRule : IParsingRule
{
    private readonly List<IParsingRule> pattern = 
    [
        new PatternRule(TokenType.UNDERSCORE),
        new PatternRule(TokenType.WORD),
        new PatternRule(TokenType.UNDERSCORE),
    ];
    
    private readonly OrRule continuesRule = new(
        [TokenType.NEW_LINE, TokenType.SPACE, TokenType.WORD]
    );
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not TextNode textNode) return null;
        if (!HasRightContinues(tokens, begin + textNode.Consumed + 2)) return null;

        return new TagNode(NodeType.ITALIC, textNode, textNode.Consumed + 2);
    }
    
    private bool HasRightContinues(List<Token> tokens, int begin)
    {
        if (tokens.Count == begin) return true;
        return continuesRule.Match(tokens, begin) is not null;
    }
}