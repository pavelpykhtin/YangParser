using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.AnyXmlStmt;

public class AnyXmlStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public AnyXmlStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyXmlStmt/data/anyxml.yang");

        var context = parser.anyxmlStmt();

        var anyXmlNode = (AnyXmlNode)_visitor.Visit(context);

        anyXmlNode.Identifier.Should().Be("context-engine-id");
        anyXmlNode.Mandatory.Should().BeTrue();
        anyXmlNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n             Applications.\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        anyXmlNode.Description.Should().Be("Dummy description");
        anyXmlNode.Config.Should().BeTrue();
        anyXmlNode.Status.Should().Be(Status.Obsolete);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyXmlStmt/data/anyxml-if.yang");

        var context = parser.anyxmlStmt();

        var anyXmlNode = (AnyXmlNode)_visitor.Visit(context);
        
        anyXmlNode.IfFeatures.Should().HaveCount(1);
        anyXmlNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyXmlStmt/data/anyxml-when.yang");

        var context = parser.anyxmlStmt();

        var anyXmlNode = (AnyXmlNode)_visitor.Visit(context);
        
        anyXmlNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        anyXmlNode.When!.Description.Should().Be("Dummy description");
        anyXmlNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AnyXmlStmt/data/anyxml-must.yang");

        var context = parser.anyxmlStmt();

        var anyXmlNode = (AnyXmlNode)_visitor.Visit(context);

        anyXmlNode.Must.Should().HaveCount(2);
        anyXmlNode.Must[0].Condition.Should().Be("be available");
        anyXmlNode.Must[0].Description.Should().Be("Dummy description");
        anyXmlNode.Must[0].Reference.Should().Be("Dummy reference");
        anyXmlNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        anyXmlNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        anyXmlNode.Must[1].Condition.Should().Be("be enabled");
    }
}