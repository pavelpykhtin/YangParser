using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.LeafStmt;

public class LeafStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public LeafStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafStmt/data/leaf.yang");

        var context = parser.leafStmt();

        var leafNode = (LeafNode)_visitor.Visit(context);

        leafNode.Identifier.Should().Be("context-engine-id");
        leafNode.Type.Identifier.Should().Be("snmp:engine-id");
        leafNode.Mandatory.Should().BeTrue();
        leafNode.Default.Should().Be("1.2.3.4");
        leafNode.Units.Should().Be("ipv4");
        leafNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n         Applications.\n         SNMP-PROXY-MIB.snmpProxyContextEngineID");
        leafNode.Description.Should().Be("Dummy description");
        leafNode.Config.Should().BeTrue();
        leafNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafStmt/data/leaf-if.yang");

        var context = parser.leafStmt();

        var leafNode = (LeafNode)_visitor.Visit(context);
        
        leafNode.IfFeatures.Should().HaveCount(1);
        leafNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafStmt/data/leaf-when.yang");

        var context = parser.leafStmt();

        var leafNode = (LeafNode)_visitor.Visit(context);
        
        leafNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        leafNode.When!.Description.Should().Be("Dummy description");
        leafNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("LeafStmt/data/leaf-must.yang");

        var context = parser.leafStmt();

        var leafNode = (LeafNode)_visitor.Visit(context);

        leafNode.Must.Should().HaveCount(2);
        leafNode.Must[0].Condition.Should().Be("be available");
        leafNode.Must[0].Description.Should().Be("Dummy description");
        leafNode.Must[0].Reference.Should().Be("Dummy reference");
        leafNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        leafNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        leafNode.Must[1].Condition.Should().Be("be enabled");
    }
}