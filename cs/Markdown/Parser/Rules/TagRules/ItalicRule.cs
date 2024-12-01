using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class ItalicRule : IParsingRule
{
    private readonly List<IParsingRule> defaultPattern =
    [
        new PatternRule(TokenType.UNDERSCORE),
        new TextRule(),
        new PatternRule(TokenType.UNDERSCORE),
    ];
    
    private readonly List<IParsingRule> innerTagPattern = 
    [
        new PatternRule(TokenType.UNDERSCORE),
        new PatternRule(TokenType.WORD),
        new PatternRule(TokenType.UNDERSCORE),
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var pattern = ChoosePattern(tokens, begin);
        var match = tokens.MatchPattern(pattern, begin);
        
        if (match.Count != pattern.Count) return null;
        if (match.Second() is not TextNode textNode) return null;
        
        var endWithWord = textNode.Last.TokenType == TokenType.WORD;
        var startWithWord = textNode.First.TokenType == TokenType.WORD;
            
        return startWithWord && endWithWord ? BuildNode(textNode) : null;
    }

    private List<IParsingRule> ChoosePattern(List<Token> tokens, int begin = 0)
    {
        if (begin != 0 && tokens[begin - 1].TokenType == TokenType.WORD)
            return innerTagPattern;
        return defaultPattern;
    }
    
    private static TagNode BuildNode(TextNode textNode) 
        => new(NodeType.ITALIC, textNode, textNode.Consumed + 2);
}