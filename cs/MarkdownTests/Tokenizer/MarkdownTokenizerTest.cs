using System.Text;
using FluentAssertions;
using Markdown.Tokenizer;

namespace MarkdownTests.Tokenizer;

[TestFixture]
public class MarkdownTokenizerTest
{
    [TestCase("Text with numbers 321")]
    [TestCase("Some not specific text")]
    [TestCase("Text with __markdown__ characters")]
    [TestCase("_A_ __lot__ of _characters_ in _markdown_\n")]
    public void MarkdownTokenizer_Tokenize_TransformAllTextToTokens(string markdown)
    {
        var tokenizer = new MarkdownTokenizer();
        
        var tokens = tokenizer.Tokenize(markdown);
        var totalLength = tokens.Sum(token => token.Length);

        totalLength.Should().Be(markdown.Length);
    }
    
    [TestCase("Hello world!")]
    [TestCase("0123456789 - this is all digits")]
    [TestCase("Some _wonderful __text with_ intersects__")]
    public void MarkdownTokenizer_Tokenize_AllTokensAreNotIntersect(string markdown)
    {
        var tokenizer = new MarkdownTokenizer();
        
        var tokens = tokenizer.Tokenize(markdown);
        
        var pairs = Enumerable
            .Range(0, tokens.Count - 1)
            .Select(i => tokens[i + 1]).Zip(tokens)
            .Select(pair => (next : pair.First, prev : pair.Second));
        pairs.Should().OnlyContain(pair => pair.next.Begin - pair.prev.Begin == pair.prev.Length);
    }

    [TestCase("Text with numbers 321")]
    [TestCase("Some not specific text")]
    [TestCase("Text with __markdown__ characters")]
    [TestCase("_A_ __lot__ of _characters_ in _markdown_\n")]
    public void MarkdownTokenizer_Tokenize_TokensPresentInCorrectOrder(string markdown)
    {
        var tokenizer = new MarkdownTokenizer();
        
        var tokens = tokenizer.Tokenize(markdown);

        var resultStringBuilder = tokens
            .Aggregate(new StringBuilder(), (sb, token) => sb.Append(token.GetValue()));
        resultStringBuilder.ToString().Should().Be(markdown);
    }
}