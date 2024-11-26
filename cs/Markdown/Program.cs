// See https://aka.ms/new-console-template for more information

using Markdown.Generator;
using Markdown.Parser;
using Markdown.Tokenizer;

namespace Markdown;

internal class Program
{
    public static void Main(string[] args)
    {
        var markdown = "This _is_ a __sample__ markdown _file_.\n";
        
        var tokens = new MarkdownTokenizer().Tokenize(markdown);
        var astRoot = new TokenParser().Parse(tokens);
        
        Console.WriteLine(new HTMLGenerator().GenerateHTML(astRoot));
    }
}