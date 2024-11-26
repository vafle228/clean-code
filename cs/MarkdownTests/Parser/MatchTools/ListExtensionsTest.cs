using FluentAssertions;
using Markdown.Parser.MatchTools;
using Markdown.Parser.MatchTools.Models;
using Markdown.Tokenizer;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Parser.MatchTools;

[TestFixture]
public class ListExtensionsTest
{
    [Test]
    public void ListExtensions_SingleMatch_EmptyPatternShouldReturnZeroMatch()
    {
        var tokens = Tokenize("Some good text here");
        var match = tokens.SingleMatch(Pattern.Empty);
        match.Should().BeEquivalentTo(Match<Token>.ZeroMatch);
    }

    [Test]
    public void ListExtensions_SingleMatch_ReturnZeroMatchOnBiggerPattern()
    {
        var tokens = Tokenize("Word");
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD], 3).End();

        var match = tokens.SingleMatch(pattern);
        
        match.Should().BeEquivalentTo(Match<Token>.ZeroMatch);
    }

    [Test]
    public void ListExtensions_SingleMatch_ReturnZeroMatchWhenThereIsNoMatch()
    {
        var tokens = Tokenize("Text with nothing to match");
        var pattern = MatchPattern
            .StartWith(TokenType.NUMBER).End();
        
        var match = tokens.SingleMatch(pattern);
        
        match.Should().BeEquivalentTo(Match<Token>.ZeroMatch);
    }

    [Test]
    public void ListExtensions_SingleMatch_ReturnMatchWhenThereIsMatch()
    {
        var tokens = Tokenize("Text with match");
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD, TokenType.SPACE], 2)
            .EndWith(TokenType.WORD);
        
        var match = tokens.SingleMatch(pattern);
        
        match.Should().NotBe(Match<Token>.ZeroMatch);
        match.Start.Should().Be(0);
        match.Length.Should().Be(tokens.Count);
    }
    
    [Test]
    public void ListExtensions_SingleMatch_ReturnMatchFromGivenBeginning()
    {
        var tokens = Tokenize("Match with non zero begin");
        var pattern = MatchPattern.Start()
            .ContinueWithRepeat([TokenType.WORD, TokenType.SPACE], 2)
            .EndWith(TokenType.WORD);
        
        var match = tokens.SingleMatch(pattern, 2);
        
        match.Should().NotBe(Match<Token>.ZeroMatch);
        match.Start.Should().Be(2);
        match.Length.Should().Be(pattern.Count);
    }
    
    [Test]
    public void ListExtensions_KleenStarMatch_ReturnMultipleMatches()
    {
        var tokens = Tokenize("123 456 789 ");
        var pattern = MatchPattern
            .StartWith(TokenType.NUMBER)
            .EndWith(TokenType.SPACE);
        
        var matches = tokens.KleenStarMatch(pattern);
        
        matches.Should().HaveCount(3);
        matches.Should().OnlyContain(m => m.Length == 2);
    }

    [Test]
    public void ListExtensions_FirstSingleMatch_ReturnFirstMatch()
    {
        var tokens = Tokenize("Some good text here");
        var twoWordsPattern = MatchPattern.Start()
            .ContinueWith([TokenType.WORD, TokenType.SPACE, TokenType.WORD])
            .End();
        var wordPattern = MatchPattern.StartWith(TokenType.WORD).End();
        
        var match = tokens.FirstSingleMatch([wordPattern, twoWordsPattern]);
        
        match.Should().NotBeEquivalentTo(Match<Token>.ZeroMatch);
        match.Start.Should().Be(0);
        match.Length.Should().Be(1);
    }

    private static List<Token> Tokenize(string text)
    {
        var tokenizer = new MarkdownTokenizer();
        return tokenizer.Tokenize(text);
    }
}