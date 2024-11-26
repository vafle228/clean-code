using FluentAssertions;
using Markdown.Parser.MatchTools;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Parser.MatchTools;

[TestFixture]
public class MatchPatternTest
{
    [Test]
    public void MatchPattern_BuildsCorrectPattern()
    {
        var patternBody = MatchPattern.StartWith(TokenType.TEXT);

        var pattern = patternBody
            .ContinueWith(TokenType.UNDERSCORE)
            .ContinueWith(TokenType.NUMBER)
            .EndWith(TokenType.NEW_LINE);

        pattern.Should().BeEquivalentTo([
            TokenType.TEXT, TokenType.UNDERSCORE,
            TokenType.NUMBER, TokenType.NEW_LINE,
        ]);
    }
}