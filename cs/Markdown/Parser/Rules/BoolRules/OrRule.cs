using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TextRules;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.BoolRules;

public class OrRule(IParsingRule firstRule, IParsingRule secondRule) : IParsingRule
{
    public OrRule(TokenType firstToken, TokenType secondToken) 
        : this(new PatternRule(firstToken), new PatternRule(secondToken))
    { }
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var firstMatch = firstRule.Match(tokens, begin);
        return firstMatch ?? secondRule.Match(tokens, begin);
    }
}