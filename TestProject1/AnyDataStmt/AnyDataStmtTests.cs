using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.AnyDataStmt;

public class AnyDataStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public AnyDataStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyDataStmt/data/anydata.yang");

        var context = parser.anydataStmt();

        var anyDataNode = (AnyDataNode)_visitor.Visit(context);

        anyDataNode.Identifier.Should().Be("context-engine-id");
        anyDataNode.Mandatory.Should().BeTrue();
        anyDataNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n             Applications.\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        anyDataNode.Description.Should().Be("Dummy description");
        anyDataNode.Config.Should().BeTrue();
        anyDataNode.Status.Should().Be(Status.Obsolete);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyDataStmt/data/anydata-if.yang");

        var context = parser.anydataStmt();

        var anyDataNode = (AnyDataNode)_visitor.Visit(context);
        
        anyDataNode.IfFeatures.Should().HaveCount(1);
        anyDataNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyDataStmt/data/anydata-when.yang");

        var context = parser.anydataStmt();

        var anyDataNode = (AnyDataNode)_visitor.Visit(context);
        
        anyDataNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        anyDataNode.When!.Description.Should().Be("Dummy description");
        anyDataNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyDataStmt/data/anydata-must.yang");

        var context = parser.anydataStmt();

        var anyDataNode = (AnyDataNode)_visitor.Visit(context);

        anyDataNode.Must.Statements.Should().HaveCount(2);
        anyDataNode.Must.Statements[0].Condition.Should().Be("be available");
        anyDataNode.Must.Statements[0].Description.Should().Be("Dummy description");
        anyDataNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        anyDataNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        anyDataNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        anyDataNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }
}