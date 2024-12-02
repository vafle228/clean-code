﻿using FluentAssertions;
using Markdown.Tokenizer.Scanners;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Tokenizer.Scanners;

[TestFixture]
public class SpecScannerTest
{
    [TestCase(" ", ExpectedResult = TokenType.SPACE)]
    [TestCase("\n", ExpectedResult = TokenType.NEW_LINE)]
    [TestCase("_", ExpectedResult = TokenType.UNDERSCORE)]
    [TestCase("Twelfth len _", 12, ExpectedResult = TokenType.UNDERSCORE)]
    public TokenType SpecScanner_Scan_ShouldScanValidTokenTypeFromText(string text, int begin = 0)
    {
        var scanner = new SpecScanner();
        
        var token = scanner.Scan(text, begin);
        
        token.Should().NotBeNull();
        return token.TokenType;
    }
    
    [TestCase("HelloWorld", 0)]
    [TestCase("IAmRegularTest", 10)]
    [TestCase("BigStringWithBigBeginNumberTest", 20)]
    public void SpecScanner_Scan_ShouldScanNullFromText(string text, int begin)
    {
        var scanner = new SpecScanner();
        var token = scanner.Scan(text, begin);
        token.Should().BeNull();
    }
}