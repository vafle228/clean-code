using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Nodes;

public record TagNode(NodeType NodeType, List<Node> Children, int Start, int Consumed) : Node(NodeType, Start, Consumed)
{
    public TagNode(NodeType nodeType, Node child, int start, int consumed) 
        : this(nodeType, [child], start, consumed)
    { }

    public string ToText(List<Token> tokens) => Children.ToText(tokens);
}