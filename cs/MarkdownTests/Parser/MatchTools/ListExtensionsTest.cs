using FluentAssertions;
using Markdown.Parser.MatchTools;
using Markdown.Tokenizer;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Parser.MatchTools;

[TestFixture]
public class ListExtensionsTest
{
    [TestCase("Some good text here")]
    public void ListExtensions_SingleMatch_EmptyPatternShouldReturnZeroMatch(string text)
    {
        var tokens = Tokenize(text);
        var match = tokens.SingleMatch([]);
        match.Should().Be(Match.ZeroMatch);
    }

    [TestCase("Word")]
    public void ListExtensions_SingleMatch_ReturnZeroMatchOnBiggerPattern(string text)
    {
        var tokens = Tokenize(text);
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD], 3).End();

        var match = tokens.SingleMatch(pattern);
        
        match.Should().Be(Match.ZeroMatch);
    }

    [TestCase("Text with nothing to match")]
    public void ListExtensions_SingleMatch_ReturnZeroMatchWhenThereIsNoMatch(string text)
    {
        var tokens = Tokenize(text);
        var pattern = MatchPattern
            .StartWith(TokenType.NUMBER).End();
        
        var match = tokens.SingleMatch(pattern);
        
        match.Should().Be(Match.ZeroMatch);
    }

    [TestCase("Text with match")]
    public void ListExtensions_SingleMatch_ReturnMatchWhenThereIsMatch(string text)
    {
        var tokens = Tokenize(text);
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD, TokenType.SPACE], 2)
            .EndWith(TokenType.WORD);
        
        var match = tokens.SingleMatch(pattern);
        
        match.Should().NotBe(Match.ZeroMatch);
        match.Start.Should().Be(0);
        match.Length.Should().Be(tokens.Count);
    }

    [TestCase("Match with non zero begin", 2)]
    public void ListExtensions_SingleMatch_ReturnMatchFromGivenBeginning(string text, int begin)
    {
        var tokens = Tokenize(text);
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD, TokenType.SPACE], 2)
            .EndWith(TokenType.WORD);
        
        var match = tokens.SingleMatch(pattern, begin);
        
        match.Should().NotBe(Match.ZeroMatch);
        match.Start.Should().Be(begin);
        match.Length.Should().Be(pattern.Count);
    }

    private static List<Token> Tokenize(string text)
    {
        var tokenizer = new MarkdownTokenizer();
        return tokenizer.Tokenize(text);
    }
}