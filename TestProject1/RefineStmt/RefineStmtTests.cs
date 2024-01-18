using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.RefineStmt;

public class RefineStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public RefineStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("RefineStmt/data/refine.yang");

        var refineStmt = parser.refineStmt();
        
        var refineNode = (RefineNode)_visitor.Visit(refineStmt);

        refineNode.Argument.Should().Be("dummy-refine");
        refineNode.Description.Should().Be("Message given at start of login session.");
        refineNode.Reference.Should().Be("Dummy reference");
        refineNode.Presence.Should().Be("Enable global loop-detect.");
        refineNode.Config.Should().BeTrue();
        refineNode.Mandatory.Should().BeTrue();
        refineNode.MinElements.Should().Be(13);
        refineNode.MaxElements.Should().Be(42);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("RefineStmt/data/refine-if.yang");

        var context = parser.refineStmt();

        var refineNode = (RefineNode)_visitor.Visit(context);
        
        refineNode.IfFeatures.Should().HaveCount(1);
        refineNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesDefaultStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("RefineStmt/data/refine-default.yang");

        var context = parser.refineStmt();

        var refineNode = (RefineNode)_visitor.Visit(context);

        refineNode.Default.Should().BeEquivalentTo("123", "456");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("RefineStmt/data/refine-must.yang");

        var context = parser.refineStmt();

        var refineNode = (RefineNode)_visitor.Visit(context);

        refineNode.Must.Statements.Should().HaveCount(2);
        refineNode.Must.Statements[0].Condition.Should().Be("be available");
        refineNode.Must.Statements[0].Description.Should().Be("Dummy description");
        refineNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        refineNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        refineNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        refineNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }
}