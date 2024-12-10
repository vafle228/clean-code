namespace Markdown.Parser.Nodes;

public record TagNode(NodeType NodeType, List<Node> Children, int Start, int Consumed) : Node(NodeType, Start, Consumed)
{
    public TagNode(NodeType nodeType, Node child, int start, int consumed) 
        : this(nodeType, [child], start, consumed)
    { }
}

public static class TagNodeFactory
{
    // public static TagNode BoldNode(Func<SpecNode, >)
}