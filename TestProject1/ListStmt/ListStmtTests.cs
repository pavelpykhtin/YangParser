using Antlr4.Runtime;
using FluentAssertions;
using YangParser;
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
        YangRfcParser parser = CreateParser("ListStmt/data/list.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Identifier.Should().Be("context-engine-id");
        listNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\r\n             Applications.\r\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
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
        YangRfcParser parser = CreateParser("ListStmt/data/list-if.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);
        
        listNode.IfFeatures.Should().HaveCount(1);
        listNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-when.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);
        
        listNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        listNode.When!.Description.Should().Be("Dummy description");
        listNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-must.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Must.Statements.Should().HaveCount(2);
        listNode.Must.Statements[0].Condition.Should().Be("be available");
        listNode.Must.Statements[0].Description.Should().Be("Dummy description");
        listNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        listNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        listNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        listNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }
    
    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-typedef.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Typedefs.Should().HaveCount(2);
        
        listNode.Typedefs[0].Identifier.Should().Be("percent");
        
        listNode.Typedefs[1].Identifier.Should().Be("minute");
    }
    
    [Fact]
    public void HandlesKeys()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-key.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Keys.Should().Contain("scope1:key1", "scope2:key2", "key3");
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-grouping.yang");

        var context = parser.listStmt();

        var listNode = (ListNode)_visitor.Visit(context);

        listNode.Groupings.Should().HaveCount(2);
        
        listNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        listNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }

    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = CreateParser("ListStmt/data/list-datadef.yang");

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
        var parser = CreateParser("ListStmt/data/list-notification.yang");

        var listStmt = parser.listStmt();
        
        var listNode = (ListNode)_visitor.Visit(listStmt);
        
        listNode.Notifications.Should().HaveCount(1);
        listNode.Notifications[0].Identifier.Should().Be("if-damp-suppress");
    }

    private YangRfcParser CreateParser(string filePath)
    {
        using var input = File.OpenText(filePath);

        AntlrInputStream inputStream = new AntlrInputStream(input);
        YangRfcLexer lexer = new YangRfcLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YangRfcParser parser = new YangRfcParser(commonTokenStream);

        return parser;
    }
}