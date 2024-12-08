﻿using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.SpecialRules;

public class ConditionalRule(IParsingRule rule, Func<Node, List<Token>, bool> condition) : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var node = rule.Match(tokens, begin);
        return node is not null && condition(node, tokens) ? node : null;
    }
}