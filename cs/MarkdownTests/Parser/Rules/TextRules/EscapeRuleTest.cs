using FluentAssertions;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.TextRules;

[TestFixture]
public class EscapeRuleTest
{
    private readonly EscapeRule rule = new();
    private readonly MarkdownTokenizer tokenizer = new();

    [TestCase(@"\_", ExpectedResult = "_")]
    [TestCase(@"\#", ExpectedResult = "#")]
    public string? EscapeRule_Match_ShouldEscapeTagsSymbols(string text)
    {
        var tokens = tokenizer.Tokenize(text);
        var match = rule.Match(tokens);
        return match?.ToText(tokens);
    }
    
    [TestCase(@"\321")]
    [TestCase(@"\Text that not escaping")]
    [TestCase(@"\ Text that not escaping")]
    public void EscapeRule_Match_ShouldNotEscapeNonTagSymbols(string text)
    {
        var tokens = tokenizer.Tokenize(text);
        var match = rule.Match(tokens);
        match.Should().BeNull();
    }
}