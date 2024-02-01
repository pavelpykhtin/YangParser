using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.IdentityStmt;

public class IdentityStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public IdentityStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("IdentityStmt/data/identity.yang");

        var context = parser.identityStmt();

        var identityNode = (IdentityNode)_visitor.Visit(context);

        identityNode.Identifier.Should().Be("dummy-identity");
        identityNode.Reference.Should().Be("Dummy reference");
        identityNode.Description.Should().Be("Dummy description");
        identityNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("IdentityStmt/data/identity-if.yang");

        var context = parser.identityStmt();

        var identityNode = (IdentityNode)_visitor.Visit(context);
        
        identityNode.IfFeatures.Should().HaveCount(1);
        identityNode.IfFeatures[0].Should().Be("ssh");
    }
    
    [Fact]
    public void HandlesIdentityRefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("IdentityStmt/data/identity-identityref.yang");

        var context = parser.identityStmt();

        var identityNode = (IdentityNode)_visitor.Visit(context);

        identityNode.Base.Should().Contain(new[]{"crypto:crypto-alg", "ssh"} );
    }
}