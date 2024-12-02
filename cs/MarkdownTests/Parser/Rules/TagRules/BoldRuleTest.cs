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

    [TestCase("Italic")]
    [TestCase("Italic tag with inner text")]
    public void BoldRule_Match_MatchWithInnerItalic(string text)
    {
        var tokens = tokenizer.Tokenize($"___{text}___");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.NodeType.Should().Be(NodeType.BOLD);
        node.Children.Should().ContainSingle(n => n.NodeType == NodeType.ITALIC);
        node.Children.First().As<TagNode>().Children.First().As<TextNode>().Text.Should().Be($"{text}");
    }

    [Test]
    public void BoldRule_Match_MatchWithInnerTextAndItalicTag()
    {
        const string text = "Default text _with inner italic_ tag";
        var tokens = tokenizer.Tokenize($"__{text}__");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.Children.Select(n => n.NodeType).Should()
            .HaveCount(3).And
            .BeEquivalentTo([NodeType.TEXT, NodeType.ITALIC, NodeType.TEXT]);
        node.NodeType.Should().Be(NodeType.BOLD);
    }
}