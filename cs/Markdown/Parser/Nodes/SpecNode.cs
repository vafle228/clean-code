namespace Markdown.Parser.Nodes;

public class SpecNode(List<Node> children, int consumed) : Node(NodeType.SPECIAL, consumed)
{
    public List<Node> Children { get; } = children;
}