using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Nodes;

public class Node(NodeType nodeType, Token value, int consumed)
{
    public Token Value { get; } = value;
    public int Consumed { get; } = consumed;
    public NodeType NodeType { get; } = nodeType;
}