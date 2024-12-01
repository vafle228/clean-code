using FluentAssertions;
using Markdown.Parser.Rules;
using Markdown.Tokenizer;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Parser.MatchTools;

[TestFixture]
public class PatternRuleTest
{
    private readonly MarkdownTokenizer tokenizer = new();
    
    [Test]
    public void PatternRule_Match_SinglePattern()
    {
        var tokens = tokenizer.Tokenize("_");
        var rule = new PatternRule([TokenType.UNDERSCORE]);
        
        var node = rule.Match(tokens);
        
        node.Should().NotBeNull();
        node.Value.Should().NotBeNull();
        node.Value.ToList().Should().BeEquivalentTo(tokens);
    }

    [Test]
    public void PatternRule_Match_ContinuesPattern()
    {
        var tokens = tokenizer.Tokenize("_\n ");
        var rule = new PatternRule([TokenType.UNDERSCORE, TokenType.NEW_LINE, TokenType.SPACE]);
        
        var node = rule.Match(tokens);
        
        node.Should().NotBeNull();
        node.Value.Should().NotBeNull();
        node.Value.ToList().Should().BeEquivalentTo(tokens);
    }
}