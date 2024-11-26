using FluentAssertions;
using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Tokenizer.Scanners;

[TestFixture]
public class SpecScannerTest
{
    [TestCase("\n", 0, ExpectedResult = TokenType.NEW_LINE)]
    [TestCase("_", 0, ExpectedResult = TokenType.UNDERSCORE)]
    [TestCase("Twelfth len _", 12, ExpectedResult = TokenType.UNDERSCORE)]
    public TokenType SpecScanner_Scan_ShouldScanValidTokenTypeFromText(string text, int begin)
    {
        var scanner = new SpecScanner();
        
        var token = scanner.Scan(text, begin);
        
        token.Should().NotBeNull();
        return token.TokenType;
    }
    
    [TestCase("Hello world", 0)]
    [TestCase("I am regular test", 10)]
    [TestCase("Big string with big begin number test", 20)]
    public void SpecScanner_Scan_ShouldScanNullFromText(string text, int begin)
    {
        var scanner = new SpecScanner();
        var token = scanner.Scan(text, begin);
        token.Should().BeNull();
    }
}