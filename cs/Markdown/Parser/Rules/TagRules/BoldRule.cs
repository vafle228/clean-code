using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class BoldRule : IParsingRule
{
    private readonly List<IParsingRule> pattern =
    [
        new PatternRule(TokenType.DOUBLE_UNDERSCORE),
        new TextRule(),
        new PatternRule(TokenType.DOUBLE_UNDERSCORE),
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not TextNode textNode) return null;
        
        var endWithWord = textNode.Last.TokenType == TokenType.WORD;
        var startWithWord = textNode.First.TokenType == TokenType.WORD;
        
        return startWithWord && endWithWord ? BuildNode(textNode) : null;
    }
    
    private static TagNode BuildNode(TextNode textNode) 
        => new(NodeType.BOLD, textNode, textNode.Consumed + 2);
}