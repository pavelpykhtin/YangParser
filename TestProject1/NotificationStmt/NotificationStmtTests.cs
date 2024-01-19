using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.NotificationStmt;

public class NotificationStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public NotificationStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("NotificationStmt/data/notification.yang");

        var notificationStmt = parser.notificationStmt();
        
        var notificationNode = (NotificationNode)_visitor.Visit(notificationStmt);

        notificationNode.Identifier.Should().Be("if-damp-suppress");
        notificationNode.Description.Should().Be("The state of interface changed from unsuppress to suppress.");
        notificationNode.Reference.Should().Be("Dummy reference");
        notificationNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("NotificationStmt/data/notification-grouping.yang");

        var context = parser.notificationStmt();

        var listNode = (NotificationNode)_visitor.Visit(context);

        listNode.Groupings.Should().HaveCount(2);
        
        listNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        listNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }

    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("NotificationStmt/data/notification-datadef.yang");

        var context = parser.notificationStmt();

        var listNode = (NotificationNode)_visitor.Visit(context);
        var containerNode = (ContainerNode)listNode.DataDefinitions[0];
        var leafNode = (LeafNode)listNode.DataDefinitions[1];

        listNode.DataDefinitions.Should().HaveCount(2);
        
        containerNode.Identifier.Should().Be("nested-container");
        containerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("NotificationStmt/data/notification-if.yang");

        var context = parser.notificationStmt();

        var notificationNode = (NotificationNode)_visitor.Visit(context);
        
        notificationNode.IfFeatures.Should().HaveCount(1);
        notificationNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("NotificationStmt/data/notification-must.yang");

        var context = parser.notificationStmt();

        var notificationNode = (NotificationNode)_visitor.Visit(context);

        notificationNode.Must.Should().HaveCount(2);
        notificationNode.Must[0].Condition.Should().Be("be available");
        notificationNode.Must[0].Description.Should().Be("Dummy description");
        notificationNode.Must[0].Reference.Should().Be("Dummy reference");
        notificationNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        notificationNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        notificationNode.Must[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("NotificationStmt/data/notification-typedef.yang");

        var context = parser.notificationStmt();

        var notificationNode = (NotificationNode)_visitor.Visit(context);

        notificationNode.Typedefs.Should().HaveCount(2);
        notificationNode.Typedefs[0].Identifier.Should().Be("percent");
        
        notificationNode.Typedefs[1].Identifier.Should().Be("minute");
    }
}