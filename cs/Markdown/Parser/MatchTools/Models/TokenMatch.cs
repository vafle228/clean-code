using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.MatchTools.Models;

public class TokenMatch(int start, int length, List<Token> source)
{
    public int Start { get; } = start;
    public int Length { get; } = length;
    public int End { get; } = start + length;
    
    public bool IsEmpty => Start == 0 && Length == 0;
    public static TokenMatch ZeroMatch => new(0, 0, []);

    public List<Token> ToList() 
        => source.Skip(Start).Take(Length).ToList();
}