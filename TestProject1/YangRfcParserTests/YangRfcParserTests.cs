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
    [InlineData("YangRfcParserTests/feature.yang", nameof(YangRfcParser.featureStmt))]
    public void Simple(string fileName, string contextAccessor)
    {
        YangRfcParser parser = CreateParser(fileName);

        var context = (IParseTree)typeof(YangRfcParser).GetMethod(contextAccessor)?.Invoke(parser, null)!;

        _visitor.Visit(context);
    }

    [Fact]
    public void HandlesFeature()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/feature.yang");

        var context = parser.featureStmt();

        var featureNode = (FeatureNode)_visitor.Visit(context);

        featureNode.Description.Should().Be("A server implements this feature if it can act as an SNMP proxy.");
        featureNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP)\r\n       Applications");
        featureNode.Status.Should().Be(Status.Current);
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