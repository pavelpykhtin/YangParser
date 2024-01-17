using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.CaseStmt;

public class CaseStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public CaseStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("CaseStmt/data/case.yang");

        var caseStmt = parser.caseStmt();
        
        var caseNode = (CaseNode)_visitor.Visit(caseStmt);

        caseNode.Identifier.Should().Be("login");
        caseNode.Description.Should().Be("Dummy description");
        caseNode.Reference.Should().Be("Dummy reference");
        caseNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("CaseStmt/data/case-if.yang");

        var context = parser.caseStmt();

        var caseNode = (CaseNode)_visitor.Visit(context);
        
        caseNode.IfFeatures.Should().HaveCount(1);
        caseNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("CaseStmt/data/case-when.yang");

        var context = parser.caseStmt();

        var caseNode = (CaseNode)_visitor.Visit(context);
        
        caseNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        caseNode.When!.Description.Should().Be("Dummy description");
        caseNode.When!.Reference.Should().Be("Dummy reference");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("CaseStmt/data/case-datadef.yang");

        var context = parser.caseStmt();

        var caseNode = (CaseNode)_visitor.Visit(context);
        var nestedContainerNode = (ContainerNode)caseNode.DataDefinitions[0];
        var leafNode = (LeafNode)caseNode.DataDefinitions[1];

        caseNode.DataDefinitions.Should().HaveCount(2);
        
        nestedContainerNode.Identifier.Should().Be("nested-container");
        nestedContainerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }
}