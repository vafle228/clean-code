using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TagRules;
using Markdown.Parser.Tools;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.TagRules;

[TestFixture]
public class BodyRuleTest
{
    private readonly BodyRule rule = new();
    private readonly MarkdownTokenizer tokenizer = new();
    
    [Test]
    public void BodyRule_Match_FullTextBody()
    {
        const string text = 
            """
            This is a sample text with a paragraph. 
            A lot of text and no tags at all!
            """;
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;

        node.Should().NotBeNull();
        node.ToText(tokens).Should().Be(text.Replace("\n", ""));
        node.Children.Should().OnlyContain(n => n.NodeType == NodeType.PARAGRAPH);
    }

    [Test]
    public void BodyRule_Match_TextWithParagraphAndHeadlineShouldMatch()
    {
        const string text = 
            """
            # This is text with headline.
            And small paragraph.
            """;
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.Children.Select(n => n.NodeType).Should().BeEquivalentTo(
            [NodeType.HEADLINE, NodeType.PARAGRAPH], options => options.WithStrictOrdering());
        node.ToText(tokens).Should().Be("This is text with headline.\rAnd small paragraph.");
    }

    [Test]
    public void BodyRule_Match_EscapedHeadlineTagShouldMatch()
    {
        const string text = 
            """
            \# This is text with escaped headline.
            And small paragraph.
            """;
        var tokens = tokenizer.Tokenize($"{text}\n");
        
        var node = rule.Match(tokens) as TagNode;
        
        node.Should().NotBeNull();
        node.Children.Select(n => n.NodeType).Should().BeEquivalentTo(
            [NodeType.ESCAPE, NodeType.PARAGRAPH, NodeType.PARAGRAPH], options => options.WithStrictOrdering());
        node.ToText(tokens).Should().Be("# This is text with escaped headline.\rAnd small paragraph.");
    }
}