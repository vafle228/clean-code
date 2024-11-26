using FluentAssertions;
using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Tokenizer.Scanners;

[TestFixture]
public class TextScannerTest
{
    [TestCase("a", 0)]
    [TestCase("_HelloWorld_", 1)]
    [TestCase("VeryBigAndCoolWord", 0)]
    public void TextScanner_Scan_TokenShouldHaveTextTokenType(string text, int begin)
    {
        var scanner = new TextScanner();
        
        var token = scanner.Scan(text, begin);

        token.Should().NotBeNull();
        token.TokenType.Should().Be(TokenType.TEXT);
    }

    [TestCase("", 0)]
    [TestCase("_", 0)]
    [TestCase("_HelloWorld_", 11)]
    public void TextScanner_Scan_ShouldScanNullFromText(string text, int begin)
    {
        var scanner = new TextScanner();
        var token = scanner.Scan(text, begin);
        token.Should().BeNull();
    }
}