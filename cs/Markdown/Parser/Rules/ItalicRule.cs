using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules;

public class ItalicRule : IParsingRule
{
    private readonly List<IParsingRule> pattern =
    [
        new PatternRule([TokenType.UNDERSCORE]),
        new TextRule(),
        new PatternRule(TokenType.UNDERSCORE),
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
        => new(NodeType.ITALIC, textNode, textNode.Consumed + 2);
}