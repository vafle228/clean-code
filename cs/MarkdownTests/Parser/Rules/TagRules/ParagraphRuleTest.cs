using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TagRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.TagRules;

[TestFixture]
public class ParagraphRuleTest
{
    private readonly ParagraphRule rule = new();
    private readonly MarkdownTokenizer tokenizer = new();
    
    [TestCase("OneWordSentenceParagraph")]
    [TestCase("Simple sentence paragraph")]
    public void ParagraphRule_Match_SimpleTextParagraphCase(string text)
    {
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;

        node.Should().NotBeNull();
        node.NodeType.Should().Be(NodeType.PARAGRAPH);
        node.Consumed.Should().Be(tokens.Count);
        node.ToText(tokens).Should().Be(text);
    }

    [Test]
    public void ParagraphRule_Match_ParagraphWithInnerTags()
    {
        const string text = "Paragraph with _italic_ and __bold__ tags";
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.NodeType.Should().Be(NodeType.PARAGRAPH);
        node.Consumed.Should().Be(tokens.Count);
        node.Children.Select(n => n.NodeType).Should().BeEquivalentTo(
            [NodeType.TEXT, NodeType.ITALIC, NodeType.TEXT, NodeType.BOLD, NodeType.TEXT],
            options => options.WithStrictOrdering());
        node.Children.First(n => n.NodeType == NodeType.BOLD).ToText(tokens).Should().Be("bold");
        node.Children.First(n => n.NodeType == NodeType.ITALIC).ToText(tokens).Should().Be("italic");
    }
    
    [TestCase("__bold and _italic__ intersection_")]
    [TestCase("Sentence with _italic and __bold_ intersection__")]
    public void ParagraphRule_Match_FullyTextParagraphCase(string text)
    {
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.ToText(tokens).Should().Be(text);
        node.NodeType.Should().Be(NodeType.PARAGRAPH);
        node.Children.Should().OnlyContain(n => n.NodeType == NodeType.TEXT);
    }
}