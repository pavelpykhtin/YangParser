using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.LeafListStmt;

public class LeafListStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public LeafListStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafListStmt/data/leaf-list.yang");

        var context = parser.leafListStmt();

        var leafListNode = (LeafListNode)_visitor.Visit(context);

        leafListNode.Identifier.Should().Be("context-engine-id");
        leafListNode.Type.Identifier.Should().Be("snmp:engine-id");
        leafListNode.Default.Should().Be("1.2.3.4");
        leafListNode.Units.Should().Be("ipv4");
        leafListNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n             Applications.\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        leafListNode.Description.Should().Be("Dummy description");
        leafListNode.MinElements.Should().Be(13);
        leafListNode.MaxElements.Should().Be(42);
        leafListNode.OrderedBy.Should().Be(OrderedBy.System);
        leafListNode.Config.Should().BeTrue();
        leafListNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafListStmt/data/leaf-list-if.yang");

        var context = parser.leafListStmt();

        var leafListNode = (LeafListNode)_visitor.Visit(context);
        
        leafListNode.IfFeatures.Should().HaveCount(1);
        leafListNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafListStmt/data/leaf-list-when.yang");

        var context = parser.leafListStmt();

        var leafListNode = (LeafListNode)_visitor.Visit(context);
        
        leafListNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        leafListNode.When!.Description.Should().Be("Dummy description");
        leafListNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafListStmt/data/leaf-list-must.yang");

        var context = parser.leafListStmt();

        var leafListNode = (LeafListNode)_visitor.Visit(context);

        leafListNode.Must.Statements.Should().HaveCount(2);
        leafListNode.Must.Statements[0].Condition.Should().Be("be available");
        leafListNode.Must.Statements[0].Description.Should().Be("Dummy description");
        leafListNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        leafListNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        leafListNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        leafListNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }
}