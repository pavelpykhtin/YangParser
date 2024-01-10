using Antlr4.Runtime;
using FluentAssertions;
using YangParser;
using YangParser.Model;

namespace TestProject1.GroupingStmt;

public class GroupingStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public GroupingStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = CreateParser("GroupingStmt/data/grouping.yang");

        var groupingStmt = parser.groupingStmt();
        
        var groupingNode = (GroupingNode)_visitor.Visit(groupingStmt);

        groupingNode.Identifier.Should().Be("login");
        groupingNode.Description.Should().Be("Message given at start of login session.");
        groupingNode.Reference.Should().Be("Dummy reference");
        groupingNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = CreateParser("GroupingStmt/data/grouping-typedef.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);

        groupingNode.Typedefs.Should().HaveCount(2);
        groupingNode.Typedefs[0].Identifier.Should().Be("percent");
        
        groupingNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesNestedGroupings()
    {
        YangRfcParser parser = CreateParser("GroupingStmt/data/grouping-grouping.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);

        groupingNode.Groupings.Should().HaveCount(2);
        groupingNode.Groupings[0].Identifier.Should().Be("nested1");
        groupingNode.Groupings[0].Description.Should().Be("nested description 1");
        
        groupingNode.Groupings[1].Identifier.Should().Be("nested2");
        groupingNode.Groupings[1].Description.Should().Be("nested description 2");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = CreateParser("GroupingStmt/data/grouping-datadef.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);
        var containerNode = (ContainerNode)groupingNode.DataDefinitions[0];
        var leafNode = (LeafNode)groupingNode.DataDefinitions[1];

        groupingNode.DataDefinitions.Should().HaveCount(2);
        
        containerNode.Description.Should().Be("container description");
        containerNode.Identifier.Should().Be("nested-container");
        
        leafNode.Description.Should().Be("leaf description");
        leafNode.Identifier.Should().Be("nested-leaf");
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