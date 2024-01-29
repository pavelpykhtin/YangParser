using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.UsesStmt;

public class UsesStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public UsesStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("UsesStmt/data/uses.yang");

        var context = parser.usesStmt();

        var usesNode = (UsesNode)_visitor.Visit(context);

        usesNode.Identifier.Should().Be("example:context-engine-id");
        usesNode.Reference.Should().Be("Dummy reference");
        usesNode.Description.Should().Be("Dummy description");
        usesNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("UsesStmt/data/uses-if.yang");

        var context = parser.usesStmt();

        var usesNode = (UsesNode)_visitor.Visit(context);
        
        usesNode.IfFeatures.Should().HaveCount(1);
        usesNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("UsesStmt/data/uses-when.yang");

        var context = parser.usesStmt();

        var usesNode = (UsesNode)_visitor.Visit(context);
        
        usesNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        usesNode.When!.Description.Should().Be("Dummy description");
        usesNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesRefine()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("UsesStmt/data/uses-refine.yang");

        var context = parser.usesStmt();

        var usesNode = (UsesNode)_visitor.Visit(context);
        
        usesNode.Refine.Should().HaveCount(2);
        
        usesNode.Refine[0].Argument.Should().Be("foo");
        
        usesNode.Refine[1].Argument.Should().Be("bar");
    }

    [Fact]
    public void HandlesAugment()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("UsesStmt/data/uses-augment.yang");

        var context = parser.usesStmt();

        var usesNode = (UsesNode)_visitor.Visit(context);
        
        usesNode.Augment.Should().HaveCount(2);
        
        usesNode.Augment[0].Argument.Should().Be("foo");
        
        usesNode.Augment[1].Argument.Should().Be("bar");
    }
}