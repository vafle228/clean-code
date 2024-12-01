using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules;

[TestFixture]
public class ItalicRuleTest
{
    private readonly ItalicRule rule = new();
    private readonly MarkdownTokenizer tokenizer = new();
    
    [TestCase("Italic")]
    [TestCase("Italic tag with spaces")]
    public void ItalicRule_Match_SimpleMatch(string text)
    {
        var tokens = tokenizer.Tokenize($"_{text}_");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.NodeType.Should().Be(NodeType.ITALIC);
        node.Children.Should().ContainSingle(n => n.NodeType == NodeType.TEXT);
        node.Children.First().As<TextNode>().Text.Should().Be(text);
    }
}