using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.SpecialRules;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TextRules;

public class EscapeRule : IParsingRule
{
    private readonly List<TokenType> escapedTokens = 
    [
        TokenType.UNDERSCORE, TokenType.HASH_TAG
    ];
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var resultRule = new AndRule([
            new PatternRule(TokenType.BACK_SLASH),
            new OrRule(escapedTokens)
        ]);
        return resultRule.Match(tokens, begin) is SpecNode node ? BuildNode(node) : null;
    }

    private static TextNode BuildNode(SpecNode node) => new(node.End, 1);
}