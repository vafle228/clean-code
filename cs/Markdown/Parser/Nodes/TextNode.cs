﻿using Markdown.Tokenizer.Tokens;

namespace Markdown.Parser.Nodes;

public class TextNode(int start, int consumed, List<Token> source) : Node(NodeType.TEXT, consumed)
{
    private readonly Lazy<Token> firstToken = new(source.Skip(start).First);
    private readonly Lazy<Token> lastToken = new(source.Skip(start).Take(consumed).Last);
    private readonly Lazy<List<Token>> tokens = new(source.Skip(start).Take(consumed).ToList);
    
    public Token Last => lastToken.Value;
    public Token First => firstToken.Value;
    public List<Token> Tokens => tokens.Value;
    public string Text => Tokens.ToText();
}