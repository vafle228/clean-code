using Markdown.Parser.MatchTools;
using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Rules;

public class ItalicRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var pattern = MatchPattern
            .StartWith(TokenType.UNDERSCORE)
            .ContinueWith(TokenType.WORD)
            .EndWith(TokenType.UNDERSCORE);
        var match = tokens.SingleMatch(pattern, begin);
        return match.IsEmpty ? null : new Node(NodeType.ITALIC, tokens[begin + 1], 3);
    }
}