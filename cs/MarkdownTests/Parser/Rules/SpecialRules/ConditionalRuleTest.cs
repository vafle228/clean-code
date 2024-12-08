using System.Collections;
using FluentAssertions;
using Markdown.Parser.Nodes;
using Markdown.Parser.Rules.SpecialRules;
using Markdown.Parser.Rules.TextRules;
using Markdown.Parser.Rules.Tools;
using Markdown.Tokenizer;

namespace MarkdownTests.Parser.Rules.SpecialRules;

[TestFixture]
public class ConditionalRuleTest
{
    private readonly TextRule primaryRule = new();
    private readonly MarkdownTokenizer tokenizer = new();

    private static IEnumerable CasesThatMatchesPrimaryRule
    {
        get
        {
            yield return new TestCaseData("Common text");
            yield return new TestCaseData(" Strange text with spaces ");
            yield return new TestCaseData("Bigger common text with a lot of words");
        }
    }

    [Test, TestCaseSource(nameof(CasesThatMatchesPrimaryRule))]
    public void ConditionalRule_Match_ShouldMatchNodeWithRightCondition(string text)
    {
        var tokens = tokenizer.Tokenize(text);
        var rule = new ConditionalRule(primaryRule, node => node.NodeType == NodeType.TEXT);
        
        var match = rule.Match(tokens);

        match.Should().NotBeNull();
        match.ToText(tokens).Should().Be(text);
        match.NodeType.Should().Be(NodeType.TEXT);
    }
    
    [Test, TestCaseSource(nameof(CasesThatMatchesPrimaryRule))]
    public void ConditionalRule_Match_ShouldNotMatchWithWrongCondition(string text)
    {
        var tokens = tokenizer.Tokenize(text);
        var rule = new ConditionalRule(primaryRule, _ => false);
        
        var match = rule.Match(tokens);

        match.Should().BeNull();
    }
}