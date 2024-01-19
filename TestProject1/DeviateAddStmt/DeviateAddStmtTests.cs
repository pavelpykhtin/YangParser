using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.DeviateAddStmt;

public class DeviateAddStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public DeviateAddStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateAddStmt/data/deviate-add.yang");

        var context = parser.deviateAddStmt();

        var deviateAddNode = (DeviateAddNode)_visitor.Visit(context);

        deviateAddNode.Mandatory.Should().BeTrue();
        deviateAddNode.Config.Should().BeTrue();
        deviateAddNode.Units.Should().Be("minutes");
        deviateAddNode.MinElements.Should().Be(11);
        deviateAddNode.MaxElements.Should().Be(42);
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateAddStmt/data/deviate-add-must.yang");

        var context = parser.deviateAddStmt();

        var deviateAddNode = (DeviateAddNode)_visitor.Visit(context);

        deviateAddNode.Must.Should().HaveCount(2);
        deviateAddNode.Must[0].Condition.Should().Be("be available");
        deviateAddNode.Must[0].Description.Should().Be("Dummy description");
        deviateAddNode.Must[0].Reference.Should().Be("Dummy reference");
        deviateAddNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        deviateAddNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        deviateAddNode.Must[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesUnique()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateAddStmt/data/deviate-add-unique.yang");

        var context = parser.deviateAddStmt();

        var deviateAddNode = (DeviateAddNode)_visitor.Visit(context);

        deviateAddNode.UniqueConstraints.Should().HaveCount(2);
        deviateAddNode.UniqueConstraints[0].Should().Be("ip port");
        deviateAddNode.UniqueConstraints[1].Should().Be("version");
    }

    [Fact]
    public void HandlesDefaults()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviateAddStmt/data/deviate-add-default.yang");

        var context = parser.deviateAddStmt();

        var deviateAddNode = (DeviateAddNode)_visitor.Visit(context);

        deviateAddNode.Default.Should().HaveCount(2);
        deviateAddNode.Default[0].Should().Be("123");
        deviateAddNode.Default[1].Should().Be("456");
    }
}