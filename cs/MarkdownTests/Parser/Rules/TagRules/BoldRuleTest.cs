using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TagRules;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.TagRules;

[TestFixture]
public class BoldRuleTest
{
    private readonly BoldRule rule = new();
    private readonly MarkdownTokenizer tokenizer = new();
    
    [TestCase("Bold")]
    [TestCase("Some bold text with spaces")]
    public void BoldRule_Match_SimpleMatch(string text)
    {
        var tokens = tokenizer.Tokenize($"__{text}__");
        
        var node = rule.Match(tokens) as TagNode;

        node.Should().NotBeNull();
        node.NodeType.Should().Be(NodeType.BOLD);
        node.Children.Should().ContainSingle(n => n.NodeType == NodeType.TEXT);
        node.Children.First().As<TextNode>().Text.Should().Be(text);
    }
}