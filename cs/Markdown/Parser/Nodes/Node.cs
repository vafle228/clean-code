using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Nodes;

public class Node(NodeType nodeType, List<Token> value, int consumed)
{
    public int Consumed { get; } = consumed;
    public List<Token> Value { get; } = value;
    public NodeType NodeType { get; } = nodeType;
}