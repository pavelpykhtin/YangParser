using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.ModuleStmt;

public class ModuleStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public ModuleStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ModuleStmt/data/module.yang");

        var context = parser.moduleStmt();

        var moduleNode = (ModuleNode)_visitor.Visit(context);

        moduleNode.Identifier.Should().Be("dummy-module");
        moduleNode.Namespace.Should().Be("urn:example:rock");
        moduleNode.Prefix.Should().Be("example");
        moduleNode.YangVersion.Should().Be("1.1");

        moduleNode.Organization.Should().Be("dummy-organization");
        moduleNode.Contact.Should().Be("dummy-contact");
        moduleNode.Description.Should().Be("dummy-description");
        moduleNode.Reference.Should().Be("dummy-reference");
    }

    [Fact]
    public void HandlesBody()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ModuleStmt/data/module-body.yang");

        var context = parser.moduleStmt();

        var moduleNode = (ModuleNode)_visitor.Visit(context);

        moduleNode.Identifier.Should().Be("dummy-module");
        moduleNode.Body.Should().HaveCount(10);

        moduleNode.Body[0].Should().BeOfType<ExtensionNode>()
            .Which.Identifier.Should().Be("dummy-extension");
        moduleNode.Body[1].Should().BeOfType<FeatureNode>()
            .Which.Identifier.Should().Be("dummy-feature");
        moduleNode.Body[2].Should().BeOfType<IdentityNode>()
            .Which.Identifier.Should().Be("dummy-identity");
        moduleNode.Body[3].Should().BeOfType<TypedefNode>()
            .Which.Identifier.Should().Be("dummy-typedef");
        moduleNode.Body[4].Should().BeOfType<GroupingNode>()
            .Which.Identifier.Should().Be("dummy-grouping");
        moduleNode.Body[5].Should().BeOfType<LeafNode>()
            .Which.Identifier.Should().Be("dummy-datadef");
        moduleNode.Body[6].Should().BeOfType<FeatureNode>()
            .Which.Identifier.Should().Be("dummy-augment");
        moduleNode.Body[7].Should().BeOfType<RpcNode>()
            .Which.Identifier.Should().Be("dummy-rpc");
        moduleNode.Body[8].Should().BeOfType<NotificationNode>()
            .Which.Identifier.Should().Be("dummy-notification");
        moduleNode.Body[9].Should().BeOfType<FeatureNode>()
            .Which.Identifier.Should().Be("dummy-deviation");
    }

    [Fact]
    public void HandlesRevisions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ModuleStmt/data/module-revision.yang");

        var context = parser.moduleStmt();

        var moduleNode = (ModuleNode)_visitor.Visit(context);

        moduleNode.Revisions.Should().HaveCount(2);

        moduleNode.Revisions[0].Date.Should().Be(new DateOnly(2024, 01, 17));
        moduleNode.Revisions[1].Date.Should().Be(new DateOnly(2023, 12, 11));
    }
}