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
    
    [TestCase("Bold tag in wo__rd__ end", 7, ExpectedResult = "rd")]
    [TestCase("Bold tag in __wo__rd begin", 6, ExpectedResult = "wo")]
    [TestCase("Bold tag in w__or__d center", 7, ExpectedResult = "or")]
    public string BoldRule_Match_ShouldMatchTagInWord(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        
        var node = rule.Match(tokens, begin) as TagNode;
        
        node.Should().NotBeNull();
        node.Children.Should().ContainSingle(n => n.NodeType == NodeType.TEXT);
        return node.Children.First().As<TextNode>().Text;
    }
    
    [TestCase("Italic tag with__123__numbers", 6)]
    [TestCase("Numbers that separates with underscores 12__34__56", 10)]
    public void ItalicRule_Match_ShouldNotMatchTextWithNumbers(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }
    
    [TestCase("Italic tag tha__t in different wor__ds", 5)]
    [TestCase("Italic tag t__hat in different words__", 5)]
    [TestCase("Italic tag that__ in different wo__rds", 5)]
    public void ItalicRule_Match_ShouldNotMatchTagInDifferentWords(string text, int begin)
    {
        var tokens = tokenizer.Tokenize(text);
        var node = rule.Match(tokens, begin) as TagNode;
        node.Should().BeNull();
    }
}