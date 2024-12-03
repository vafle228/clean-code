using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules.TagRules;

public class BodyRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        throw new NotImplementedException();
    }
}