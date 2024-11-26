using FluentAssertions;
using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Tokenizer.Scanners;

[TestFixture]
public class TextScannerTest
{
    [TestCase(" ", 0)]
    [TestCase("_Hello world_", 1)]
    [TestCase("Some thoughts in this text", 0)]
    public void TextScanner_Scan_TokenShouldHaveTextTokenType(string text, int begin)
    {
        var scanner = new TextScanner();
        
        var token = scanner.Scan(text, begin);

        token.Should().NotBeNull();
        token.TokenType.Should().Be(TokenType.TEXT);
    }

    [TestCase("", 0)]
    [TestCase("_", 0)]
    [TestCase("_Hello world_", 12)]
    public void TextScanner_Scan_ShouldScanNullFromText(string text, int begin)
    {
        var scanner = new TextScanner();
        var token = scanner.Scan(text, begin);
        token.Should().BeNull();
    }
}