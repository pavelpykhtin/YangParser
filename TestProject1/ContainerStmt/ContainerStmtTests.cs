using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.ContainerStmt;

public class ContainerStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public ContainerStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("ContainerStmt/data/container.yang");

        var containerStmt = parser.containerStmt();
        
        var containerNode = (ContainerNode)_visitor.Visit(containerStmt);

        containerNode.Identifier.Should().Be("login");
        containerNode.Description.Should().Be("Message given at start of login session.");
        containerNode.Reference.Should().Be("Dummy reference");
        containerNode.Presence.Should().Be("Enable global loop-detect.");
        containerNode.Status.Should().Be(Status.Current);
        containerNode.Config.Should().BeTrue();
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-if.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);
        
        containerNode.IfFeatures.Should().HaveCount(1);
        containerNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-when.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);
        
        containerNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        containerNode.When!.Description.Should().Be("Dummy description");
        containerNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-must.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Must.Should().HaveCount(2);
        containerNode.Must[0].Condition.Should().Be("be available");
        containerNode.Must[0].Description.Should().Be("Dummy description");
        containerNode.Must[0].Reference.Should().Be("Dummy reference");
        containerNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        containerNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        containerNode.Must[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-typedef.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Typedefs.Should().HaveCount(2);
        containerNode.Typedefs[0].Identifier.Should().Be("percent");
        
        containerNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-grouping.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Groupings.Should().HaveCount(2);
        
        containerNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        containerNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-datadef.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);
        var nestedContainerNode = (ContainerNode)containerNode.DataDefinitions[0];
        var leafNode = (LeafNode)containerNode.DataDefinitions[1];

        containerNode.DataDefinitions.Should().HaveCount(2);
        
        nestedContainerNode.Identifier.Should().Be("nested-container");
        nestedContainerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }

    [Fact]
    public void HandlesActions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ContainerStmt/data/container-action.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Actions.Should().HaveCount(2);
        
        containerNode.Actions[0].Identifier.Should().Be("action-a");
        
        containerNode.Actions[1].Identifier.Should().Be("action-b");
    }

    [Fact]
    public void HandlesNotifications()
    {
        var parser = ParserHelpers.CreateParser("ContainerStmt/data/container-notification.yang");

        var containerStmt = parser.containerStmt();
        
        var containerNode = (ContainerNode)_visitor.Visit(containerStmt);
        
        containerNode.Notifications.Should().HaveCount(1);
        containerNode.Notifications[0].Identifier.Should().Be("if-damp-suppress");
    }
}