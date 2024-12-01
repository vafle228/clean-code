using Markdown.Parser.Nodes;
using Markdown.Tokenizer.Tokens;
using Markdown.Parser.MatchTools.Models;

namespace Markdown.Parser.Rules;

public class TextRule : IParsingRule
{
    public Node? Match(List<Token> tokens, int begin = 0)
    {
        var textLength = tokens.Skip(begin).TakeWhile(IsText).Count();
        
        if (textLength == 0) return null;
        var match = new TokenMatch(begin, textLength, tokens);
        
        return new Node(NodeType.TEXT, match, textLength);
    }

    private static bool IsText(Token token)
        => token.TokenType is TokenType.WORD or TokenType.SPACE;
}