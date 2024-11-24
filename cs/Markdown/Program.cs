// See https://aka.ms/new-console-template for more information

using Markdown.Generator;
using Markdown.Parser;
using Markdown.Tokenizer;

namespace Markdown;

internal class Program
{
    public static void Main(string[] args)
    {
        var markdown = "This is a sample markdown file.";
        
        var tokens = new MarkdownTokenizer().Tokenize(markdown);
        var astRoot = new TokenParser().Parse(tokens);
        
        Console.WriteLine(new HTMLGenerator().GenerateHTML(astRoot));
    }
}