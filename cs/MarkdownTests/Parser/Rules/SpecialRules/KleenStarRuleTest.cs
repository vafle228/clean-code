using Markdown.Parser.Rules.SpecialRules;
using Markdown.Tokenizer;
using Markdown.Tokenizer.Tokens;

namespace MarkdownTests.Parser.Rules.SpecialRules;

[TestFixture]
public class KleenStarRuleTest
{
    private readonly MarkdownTokenizer tokenizer = new();
    private OrRule primaryRule = new(TokenType.WORD, TokenType.SPACE);
    
    // [TestCase("Default text with words and spaces")]
}