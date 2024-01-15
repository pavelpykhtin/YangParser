using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.OutputStmt;

public class OutputStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public OutputStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("OutputStmt/data/output-must.yang");

        var context = parser.outputStmt();

        var outputNode = (OutputNode)_visitor.Visit(context);

        outputNode.Must.Statements.Should().HaveCount(2);
        outputNode.Must.Statements[0].Condition.Should().Be("be available");
        outputNode.Must.Statements[0].Description.Should().Be("Dummy description");
        outputNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        outputNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        outputNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        outputNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("OutputStmt/data/output-typedef.yang");

        var context = parser.outputStmt();

        var outputNode = (OutputNode)_visitor.Visit(context);

        outputNode.Typedefs.Should().HaveCount(2);
        outputNode.Typedefs[0].Identifier.Should().Be("percent");
        
        outputNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("OutputStmt/data/output-grouping.yang");

        var context = parser.outputStmt();

        var outputNode = (OutputNode)_visitor.Visit(context);

        outputNode.Groupings.Should().HaveCount(2);
        
        outputNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        outputNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("OutputStmt/data/output-datadef.yang");

        var context = parser.outputStmt();

        var outputNode = (OutputNode)_visitor.Visit(context);
        var nestedContainerNode = (ContainerNode)outputNode.DataDefinitions[0];
        var leafNode = (LeafNode)outputNode.DataDefinitions[1];

        outputNode.DataDefinitions.Should().HaveCount(2);
        
        nestedContainerNode.Identifier.Should().Be("nested-container");
        nestedContainerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }
}