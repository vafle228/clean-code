using Markdown.Parser.MatchTools.Models;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Nodes;

public class Node(NodeType nodeType, List<Node> children, int consumed, TokenMatch? value = null)
{
    public int Consumed { get; } = consumed;
    public TokenMatch? Value { get; } = value;
    public NodeType NodeType { get; } = nodeType;
    public List<Node> Children { get; } = children;
    
    public Node(NodeType nodeType, TokenMatch value, int consumed)
        : this(nodeType, [], consumed, value) {}
}