using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.DeviateDeleteStmt;

public class DeviateDeleteStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public DeviateDeleteStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateDeleteStmt/data/deviate-delete.yang");

        var context = parser.deviateDeleteStmt();

        var deviateDeleteNode = (DeviateDeleteNode)_visitor.Visit(context);

        deviateDeleteNode.Units.Should().Be("minutes");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateDeleteStmt/data/deviate-delete-must.yang");

        var context = parser.deviateDeleteStmt();

        var deviateDeleteNode = (DeviateDeleteNode)_visitor.Visit(context);

        deviateDeleteNode.Must.Should().HaveCount(2);
        deviateDeleteNode.Must[0].Condition.Should().Be("be available");
        deviateDeleteNode.Must[0].Description.Should().Be("Dummy description");
        deviateDeleteNode.Must[0].Reference.Should().Be("Dummy reference");
        deviateDeleteNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        deviateDeleteNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        deviateDeleteNode.Must[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesUnique()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateDeleteStmt/data/deviate-delete-unique.yang");

        var context = parser.deviateDeleteStmt();

        var deviateDeleteNode = (DeviateDeleteNode)_visitor.Visit(context);

        deviateDeleteNode.UniqueConstraints.Should().HaveCount(2);
        deviateDeleteNode.UniqueConstraints[0].Should().Be("ip port");
        deviateDeleteNode.UniqueConstraints[1].Should().Be("version");
    }

    [Fact]
    public void HandlesDefaults()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateDeleteStmt/data/deviate-delete-default.yang");

        var context = parser.deviateDeleteStmt();

        var deviateDeleteNode = (DeviateDeleteNode)_visitor.Visit(context);

        deviateDeleteNode.Default.Should().HaveCount(2);
        deviateDeleteNode.Default[0].Should().Be("123");
        deviateDeleteNode.Default[1].Should().Be("456");
    }
}