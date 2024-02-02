using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.DeviateReplaceStmt;

public class DeviateReplaceStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public DeviateReplaceStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateReplaceStmt/data/deviate-replace.yang");

        var context = parser.deviateReplaceStmt();

        var deviateReplaceNode = (DeviateReplaceNode)_visitor.Visit(context);

        deviateReplaceNode.Mandatory.Should().BeTrue();
        deviateReplaceNode.Config.Should().BeTrue();
        deviateReplaceNode.Units.Should().Be("minutes");
        deviateReplaceNode.MinElements.Should().Be(11);
        deviateReplaceNode.MaxElements.Should().Be(42);
    }

    [Fact]
    public void HandlesDefaults()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateReplaceStmt/data/deviate-replace-default.yang");

        var context = parser.deviateReplaceStmt();

        var deviateReplaceNode = (DeviateReplaceNode)_visitor.Visit(context);

        deviateReplaceNode.Default.Should().HaveCount(2);
        deviateReplaceNode.Default[0].Should().Be("123");
        deviateReplaceNode.Default[1].Should().Be("456");
    }

    [Fact]
    public void HandlesType()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateReplaceStmt/data/deviate-replace-type.yang");

        var context = parser.deviateReplaceStmt();

        var deviateReplaceNode = (DeviateReplaceNode)_visitor.Visit(context);

        deviateReplaceNode.Type!.Identifier.Should().Be("uint32");
    }
}