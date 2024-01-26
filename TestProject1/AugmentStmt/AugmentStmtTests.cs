using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.AugmentStmt;

public class AugmentStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public AugmentStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);

        augmentNode.Argument.Should().Be("context-engine-id");
        augmentNode.Reference.Should().Be("Dummy reference");
        augmentNode.Description.Should().Be("Dummy description");
        augmentNode.Status.Should().Be(Status.Obsolete);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-if.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);
        
        augmentNode.IfFeatures.Should().HaveCount(1);
        augmentNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-when.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);
        
        augmentNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        augmentNode.When!.Description.Should().Be("Dummy description");
        augmentNode.When!.Reference.Should().Be("Dummy reference");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-datadef.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);
        var nestedContainerNode = (ContainerNode)augmentNode.DataDefinitions[0];
        var leafNode = (LeafNode)augmentNode.DataDefinitions[1];

        augmentNode.DataDefinitions.Should().HaveCount(2);
        
        nestedContainerNode.Identifier.Should().Be("nested-container");
        nestedContainerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }
    
    [Fact]
    public void HandlesCaseStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-case.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);

        augmentNode.Cases.Should().HaveCount(2);
        
        augmentNode.Cases[0].Identifier.Should().Be("case-1");
        augmentNode.Cases[0].Description.Should().Be("case description");
        
        augmentNode.Cases[1].Identifier.Should().Be("case-2");
    }

    [Fact]
    public void HandlesActions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-action.yang");

        var context = parser.augmentStmt();

        var augmentNode = (AugmentNode)_visitor.Visit(context);

        augmentNode.Actions.Should().HaveCount(2);
        
        augmentNode.Actions[0].Identifier.Should().Be("action-a");
        
        augmentNode.Actions[1].Identifier.Should().Be("action-b");
    }

    [Fact]
    public void HandlesNotifications()
    {
        var parser = ParserHelpers.CreateParser("AugmentStmt/data/augment-notification.yang");

        var augmentStmt = parser.augmentStmt();
        
        var augmentNode = (AugmentNode)_visitor.Visit(augmentStmt);
        
        augmentNode.Notifications.Should().HaveCount(1);
        augmentNode.Notifications[0].Identifier.Should().Be("if-damp-suppress");
    }
}