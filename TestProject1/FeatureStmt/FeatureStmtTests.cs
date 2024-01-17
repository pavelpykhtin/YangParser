using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.FeatureStmt;

public class FeatureStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public FeatureStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("FeatureStmt/data/feature.yang");

        var context = parser.featureStmt();

        var featureNode = (FeatureNode)_visitor.Visit(context);

        featureNode.Description.Should().Be("A server implements this feature if it can act as an SNMP proxy.");
        featureNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP)\r\n       Applications");
        featureNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("FeatureStmt/data/feature-if.yang");
        
        var context = parser.featureStmt();
        
        var featureNode = (FeatureNode)_visitor.Visit(context);
        
        featureNode.IfFeatures.Should().HaveCount(1);
        featureNode.IfFeatures[0].Should().Be("ssh");

    }
}