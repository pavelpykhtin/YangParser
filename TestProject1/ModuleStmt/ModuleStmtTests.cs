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
    public void HandlesBody()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ModuleStmt/data/module.yang");

        var context = parser.moduleStmt();

        var moduleNode = (ModuleNode)_visitor.Visit(context);
        
        moduleNode.Identifier.Should().Be("dummy-module");
        moduleNode.Body.Should().HaveCount(10);
        
        moduleNode.Body[0].Should().BeOfType<FeatureNode>()
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
}