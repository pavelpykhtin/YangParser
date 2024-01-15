using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using YangParser;
using YangParser.Model;

namespace TestProject1.YangRfcParserTests;

public class YangRfcParserTests
{
    private readonly YangRfcVisitor _visitor;

    public YangRfcParserTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Theory]
    [InlineData("YangRfcParserTests/sample1.yang", nameof(YangRfcParser.moduleStmt))]
    public void Simple(string fileName, string contextAccessor)
    {
        YangRfcParser parser = CreateParser(fileName);

        var context = (IParseTree)typeof(YangRfcParser).GetMethod(contextAccessor)?.Invoke(parser, null)!;

        _visitor.Visit(context);
    }

    private YangRfcParser CreateParser(string filePath)
    {
        using var input = File.OpenText(filePath);

        AntlrInputStream inputStream = new AntlrInputStream(input);
        YangRfcLexer lexer = new YangRfcLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YangRfcParser parser = new YangRfcParser(commonTokenStream);

        return parser;
    }
}