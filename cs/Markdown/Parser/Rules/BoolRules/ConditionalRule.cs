using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TextRules;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.BoolRules;

public class ConditionalRule(IParsingRule rule, Func<Node, bool> condition) : IParsingRule
{
    public ConditionalRule(TokenType tokenType, Func<Node, bool> condition)
        : this(new PatternRule(tokenType), condition) 
    { }
    
    public ConditionalRule(List<IParsingRule> rules, Func<Node, bool> condition)
        : this(new AndRule(rules), condition) 
    { }
    
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var node = rule.Match(tokens, begin);
        return node is not null && condition(node) ? node : null;
    }
}