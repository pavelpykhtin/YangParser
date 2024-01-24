using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.ListStmt;

public class ListStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public ListStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Identifier.Should().Be("context-engine-id");
        listNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n             Applications.\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        listNode.Description.Should().Be("Dummy description");
        listNode.MinElements.Should().Be(13);
        listNode.MaxElements.Should().Be(42);
        listNode.OrderedBy.Should().Be(OrderedBy.System);
        listNode.Config.Should().BeTrue();
        listNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-if.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);
        
        listNode.IfFeatures.Should().HaveCount(1);
        listNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-when.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);
        
        listNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        listNode.When!.Description.Should().Be("Dummy description");
        listNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-must.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Must.Should().HaveCount(2);
        listNode.Must[0].Condition.Should().Be("be available");
        listNode.Must[0].Description.Should().Be("Dummy description");
        listNode.Must[0].Reference.Should().Be("Dummy reference");
        listNode.Must[0].ErrorMessage.Should().Be("Dummy error message");
        listNode.Must[0].ErrorAppTag.Should().Be("Dummy error app tag");

        listNode.Must[1].Condition.Should().Be("be enabled");
    }
    
    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-typedef.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Typedefs.Should().HaveCount(2);
        
        listNode.Typedefs[0].Identifier.Should().Be("percent");
        
        listNode.Typedefs[1].Identifier.Should().Be("minute");
    }
    
    [Fact]
    public void HandlesKeys()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-key.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Keys.Should().Contain("scope1:key1", "scope2:key2", "key3");
    }
    
    [Fact]
    public void HandlesQuotedKeys()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-key-quoted.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Keys.Should().Contain("scope1:key1", "scope2:key2", "key3");
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-grouping.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Groupings.Should().HaveCount(2);
        
        listNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        listNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }

    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-datadef.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);
        var containerNode = (ContainerNode)listNode.DataDefinitions[0];
        var leafNode = (LeafNode)listNode.DataDefinitions[1];

        listNode.DataDefinitions.Should().HaveCount(2);
        
        containerNode.Identifier.Should().Be("nested-container");
        containerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }

    [Fact]
    public void HandlesNotifications()
    {
        var parser = ParserHelpers.CreateParser("ListStmt/data/list-notification.yang");

        var listStmt = parser.listStmt();
        
        var listNode = (ListNode)_visitor.Visit(listStmt);
        
        listNode.Notifications.Should().HaveCount(1);
        listNode.Notifications[0].Identifier.Should().Be("if-damp-suppress");
    }

    [Fact]
    public void HandlesActions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-action.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Actions.Should().HaveCount(2);
        
        listNode.Actions[0].Identifier.Should().Be("action-a");
        
        listNode.Actions[1].Identifier.Should().Be("action-b");
    }

    [Fact]
    public void HandlesUnique()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ListStmt/data/list-unique.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.UniqueConstraints.Should().HaveCount(2);
        
        listNode.UniqueConstraints[0].Should().Be("ip port");
        listNode.UniqueConstraints[1].Should().Be("version");
    }
}