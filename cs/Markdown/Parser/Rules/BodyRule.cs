﻿using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules;

public class BodyRule : IParsingRule
{
    public Node Match(List<Token> tokens)
    {
        throw new NotImplementedException();
    }
}