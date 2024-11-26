using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules;

[TestFixture]
public class ItalicRuleTest
{
    [TestCase("_Italic_")]
    [TestCase("_ThisIsAllItalicText_")]
    public void ItalicRule_Match_SimpleMatch(string markdown)
    {
        var rule = new ItalicRule();
        var tokenizer = new MarkdownTokenizer();
        var tokens = tokenizer.Tokenize(markdown);
        
        var node = rule.Match(tokens);

        node.Should().NotBeNull();
        node.Consumed.Should().Be(3);
        node.NodeType.Should().Be(NodeType.ITALIC);
        node.Value.Should().BeEquivalentTo(tokens[1]);
    }

    [TestCase("_Italic tag with spaces_")]
    public void ItalicRule_Match_MatchWithSpaces(string markdown)
    {
        var rule = new ItalicRule();
        var tokenizer = new MarkdownTokenizer();
        var tokens = tokenizer.Tokenize(markdown);
        
        var node = rule.Match(tokens);
        
        
    }
}