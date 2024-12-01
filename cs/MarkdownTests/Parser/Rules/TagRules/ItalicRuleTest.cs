using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.TagRules;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.TagRules;

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
    [TestCase("Italic tag t_hat in different words_", 5)]
    [TestCase("Italic tag that_ in different wo_rds", 5)]
    public void ItalicRule_Match_ShouldNotMatchTagInDifferentWords(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }

    [TestCase("Italic tag that_ in different_ words", 5)]
    [TestCase("Italic tag that _ in different words_", 6)]
    public void ItalicRule_Match_ShouldNotMatchWhenSpaceAfterOpenTag(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }
    
    [TestCase("Italic tag that _in different _words", 6)]
    [TestCase("Italic tag that _in different words _", 6)]
    public void ItalicRule_Match_ShouldNotMatchWhenSpaceBeforeCloseTag(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }

    [TestCase("Not pair __symbols case_", 4)]
    [TestCase("Not pair _symbols case__", 4)]
    public void ItalicRule_Match_DifferentUnderscoresShouldNotMatch(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }
}