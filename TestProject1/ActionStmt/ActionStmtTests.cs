using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.ActionStmt;

public class ActionStmtTests
{
    private const string Folder = "ActionStmt";
    private const string Prefix = "action";
    private readonly YangRfcVisitor _visitor;

    public ActionStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}.yang");

        var actionStmt = parser.actionStmt();
        
        var actionNode = (ActionNode)_visitor.Visit(actionStmt);

        actionNode.Identifier.Should().Be("ping");
        actionNode.Description.Should().Be("Dummy description");
        actionNode.Reference.Should().Be("Dummy reference");
        actionNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesGroupings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-grouping.yang");

        var context = parser.actionStmt();

        var listNode = (ActionNode)_visitor.Visit(context);

        listNode.Groupings.Should().HaveCount(2);
        
        listNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        listNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-if.yang");

        var context = parser.actionStmt();

        var actionNode = (ActionNode)_visitor.Visit(context);
        
        actionNode.IfFeatures.Should().HaveCount(1);
        actionNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-typedef.yang");

        var context = parser.actionStmt();

        var actionNode = (ActionNode)_visitor.Visit(context);

        actionNode.Typedefs.Should().HaveCount(2);
        actionNode.Typedefs[0].Identifier.Should().Be("percent");
        
        actionNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesInputsAndOutputs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-input-output.yang");

        var context = parser.actionStmt();

        var actionNode = (ActionNode)_visitor.Visit(context);

        actionNode.Input!.DataDefinitions.Should().HaveCount(1);
        ((LeafNode)actionNode.Input.DataDefinitions[0]).Identifier.Should().Be("input-value");
        ((LeafNode)actionNode.Input.DataDefinitions[0]).Type.Identifier.Should().Be("string");
        
        actionNode.Output!.DataDefinitions.Should().HaveCount(1);
        ((LeafNode)actionNode.Output.DataDefinitions[0]).Identifier.Should().Be("output-value");
        ((LeafNode)actionNode.Output.DataDefinitions[0]).Type.Identifier.Should().Be("uint32");
    }
}