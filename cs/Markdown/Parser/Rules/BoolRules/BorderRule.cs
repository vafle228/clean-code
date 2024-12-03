using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.BoolRules;

public class BorderRule(IParsingRule rule, Func<Node, bool> condition) : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0) => rule.Match(tokens, begin) switch 
    { 
        null => null, 
        TextNode textNode => condition(textNode) ? textNode : null, 
        TagNode tagNode => CheckList(tagNode.Children) ? tagNode : null, 
        SpecNode specNode => CheckList(specNode.Children) ? specNode : null, 
        
        _ => throw new ArgumentOutOfRangeException()
    };
    
    private bool CheckList(List<Node> nodes) 
        => condition(nodes.First()) && condition(nodes.Last());

    public static BorderRule NoSpaceOnBorderRule(IParsingRule pattern)
        => new(pattern, node =>
        {
            if (node is not TextNode textNode) return true;
            return textNode.First.TokenType != TokenType.SPACE && textNode.Last.TokenType != TokenType.SPACE;
        });
}