using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.SubmoduleStmt;

public class SubmoduleStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public SubmoduleStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("SubmoduleStmt/data/submodule.yang");

        var context = parser.submoduleStmt();

        var submoduleNode = (SubmoduleNode)_visitor.Visit(context);

        submoduleNode.Identifier.Should().Be("dummy-submodule");
        submoduleNode.YangVersion.Should().Be("1.1");

        submoduleNode.Organization.Should().Be("dummy-organization");
        submoduleNode.Contact.Should().Be("dummy-contact");
        submoduleNode.Description.Should().Be("dummy-description");
        submoduleNode.Reference.Should().Be("dummy-reference");
    }

    [Fact]
    public void HandlesBody()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("SubmoduleStmt/data/submodule-body.yang");

        var context = parser.submoduleStmt();

        var submoduleNode = (SubmoduleNode)_visitor.Visit(context);

        submoduleNode.Identifier.Should().Be("dummy-submodule");
        submoduleNode.Body.Should().HaveCount(10);

        submoduleNode.Body[0].Should().BeOfType<ExtensionNode>()
            .Which.Identifier.Should().Be("dummy-extension");
        submoduleNode.Body[1].Should().BeOfType<FeatureNode>()
            .Which.Identifier.Should().Be("dummy-feature");
        submoduleNode.Body[2].Should().BeOfType<IdentityNode>()
            .Which.Identifier.Should().Be("dummy-identity");
        submoduleNode.Body[3].Should().BeOfType<TypedefNode>()
            .Which.Identifier.Should().Be("dummy-typedef");
        submoduleNode.Body[4].Should().BeOfType<GroupingNode>()
            .Which.Identifier.Should().Be("dummy-grouping");
        submoduleNode.Body[5].Should().BeOfType<LeafNode>()
            .Which.Identifier.Should().Be("dummy-datadef");
        submoduleNode.Body[6].Should().BeOfType<AugmentNode>()
            .Which.Argument.Should().Be("dummy-augment");
        submoduleNode.Body[7].Should().BeOfType<RpcNode>()
            .Which.Identifier.Should().Be("dummy-rpc");
        submoduleNode.Body[8].Should().BeOfType<NotificationNode>()
            .Which.Identifier.Should().Be("dummy-notification");
        submoduleNode.Body[9].Should().BeOfType<FeatureNode>()
            .Which.Identifier.Should().Be("dummy-deviation");
    }

    [Fact]
    public void HandlesRevisions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("SubmoduleStmt/data/submodule-revision.yang");

        var context = parser.submoduleStmt();

        var submoduleNode = (SubmoduleNode)_visitor.Visit(context);

        submoduleNode.Revisions.Should().HaveCount(2);

        submoduleNode.Revisions[0].Date.Should().Be(new DateOnly(2024, 01, 17));
        submoduleNode.Revisions[1].Date.Should().Be(new DateOnly(2023, 12, 11));
    }

    [Fact]
    public void HandlesIncludes()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("SubmoduleStmt/data/submodule-include.yang");

        var context = parser.submoduleStmt();

        var submoduleNode = (SubmoduleNode)_visitor.Visit(context);

        submoduleNode.Includes.Should().HaveCount(2);

        submoduleNode.Includes[0].Identifier.Should().Be("dummy-include-1");
        submoduleNode.Includes[1].Identifier.Should().Be("dummy-include-2");
    }

    [Fact]
    public void HandlesImports()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("SubmoduleStmt/data/submodule-import.yang");

        var context = parser.submoduleStmt();

        var submoduleNode = (SubmoduleNode)_visitor.Visit(context);

        submoduleNode.Imports.Should().HaveCount(2);

        submoduleNode.Imports[0].Identifier.Should().Be("dummy-import-1");
        submoduleNode.Imports[1].Identifier.Should().Be("dummy-import-2");
    }
}