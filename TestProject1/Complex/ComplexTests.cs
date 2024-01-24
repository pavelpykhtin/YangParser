using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.Complex;

public class ComplexTests
{
    private readonly YangRfcVisitor _visitor;

    public ComplexTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Theory]
    [InlineData("sample1.yang")]
    [InlineData("sample2.yang")]
    [InlineData("sample3.yang")]
    public void CanLoadSamples(string filename)
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"Complex/data/{filename}");

        var context = parser.rootStmt().moduleStmt();

        var moduleNode = (ModuleNode)_visitor.Visit(context);
        
        moduleNode.Should().NotBeNull();
    }
}