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

    [TestCase("Italic tag with_123_numbers")]
    [TestCase("Numbers that separates with underscores 12_34_56")]
    public void ItalicRule_Match_ShouldNotMatchTextWithNumbers(string text)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens) as TagNode;
        node.Should().BeNull();
    }
    
    [TestCase("Italic tag in wo_rd_ end", 7, ExpectedResult = "rd")]
    [TestCase("Italic tag in _wo_rd begin", 6, ExpectedResult = "wo")]
    [TestCase("Italic tag in w_or_d center", 7, ExpectedResult = "or")]
    public string ItalicRule_Match_ShouldMatchTagInWord(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        
        var node = rule.Match(tokens, begin) as TagNode;
        
        node.Should().NotBeNull();
        node.Children.Should().ContainSingle(n => n.NodeType == NodeType.TEXT);
        return node.Children.First().As<TextNode>().Text;
    }

    [TestCase("Italic tag tha_t in different wor_ds", 5)]
    public void ItalicRule_Match_ShouldNotMatchTagInDifferentWords(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }
}