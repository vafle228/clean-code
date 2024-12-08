// using Markdown.Parser.Nodes;
// using Markdown.Parser.Rules.SpecialRules;
// using Markdown.Parser.Rules.TextRules;
// using Markdown.Tokenizer.Tokens;
//
// namespace Markdown.Parser.Rules.TagRules;
//
// public class ItalicRule : IParsingRule
// {
//     private static readonly AndRule InnerBoldRule = new([
//         PatternRule.DoubleUnderscoreRule(),
//         new TextRule(),
//         PatternRule.DoubleUnderscoreRule()
//     ]);
//     
//     public Node? Match(List<Token> tokens, int begin = 0)
//     {
//         return !InWordItalicRule.IsTagInWord(tokens, begin)
//             ? MatchItalic(tokens, begin)
//             : new InWordItalicRule().Match(tokens, begin);
//     }
//
//     private static TagNode? MatchItalic(List<Token> tokens, int begin)
//     {
//         var valueRule = new OrRule(new TextRule(), InnerBoldRule);
//         var pattern = new AndRule([
//             new PatternRule(TokenType.UNDERSCORE),
//             new ConditionalRule(new KleenStarRule(valueRule), HasRightBorders),
//             new PatternRule(TokenType.UNDERSCORE),
//         ]);
//         var continuesRule = new OrRule([
//             PatternRule.DoubleUnderscoreRule(),
//             new PatternRule(TokenType.NEW_LINE),
//             new PatternRule(TokenType.SPACE),
//         ]);
//         
//         var resultRule = new ContinuesRule(pattern, continuesRule);
//         return resultRule.Match(tokens, begin) is SpecNode specNode ? BuildNode(specNode) : null;
//     }
//
//     private static TagNode BuildNode(SpecNode node)
//     {
//         var valueNode = (node.Nodes.Single() as SpecNode)!;
//         return new TagNode(NodeType.ITALIC, valueNode.Nodes, node.Consumed);
//     }
//
//     private static bool HasRightBorders(Node node)
//         => node is SpecNode specNode
//            && TextDontEndWithSpace(specNode.Nodes.Last())
//            && TextDontStartWithSpace(specNode.Nodes.First());
//
//     private static bool TextDontStartWithSpace(Node node)
//         => node is TextNode textNode && textNode.Last.TokenType != TokenType.SPACE;
//     
//     private static bool TextDontEndWithSpace(Node node) 
//         => node is TextNode textNode && textNode.Last.TokenType != TokenType.SPACE;
// }