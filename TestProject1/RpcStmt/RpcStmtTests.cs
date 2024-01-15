using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.RpcStmt;

public class RpcStmtTests
{
    private const string Folder = "RpcStmt";
    private const string Prefix = "rpc";
    private readonly YangRfcVisitor _visitor;

    public RpcStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}.yang");

        var rpcStmt = parser.rpcStmt();
        
        var rpcNode = (RpcNode)_visitor.Visit(rpcStmt);

        rpcNode.Identifier.Should().Be("ping");
        rpcNode.Description.Should().Be("Dummy description");
        rpcNode.Reference.Should().Be("Dummy reference");
        rpcNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-grouping.yang");

        var context = parser.rpcStmt();

        var listNode = (RpcNode)_visitor.Visit(context);

        listNode.Groupings.Should().HaveCount(2);
        
        listNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        listNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-if.yang");

        var context = parser.rpcStmt();

        var rpcNode = (RpcNode)_visitor.Visit(context);
        
        rpcNode.IfFeatures.Should().HaveCount(1);
        rpcNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-typedef.yang");

        var context = parser.rpcStmt();

        var rpcNode = (RpcNode)_visitor.Visit(context);

        rpcNode.Typedefs.Should().HaveCount(2);
        rpcNode.Typedefs[0].Identifier.Should().Be("percent");
        
        rpcNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesInputsAndOutputs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}-input-output.yang");

        var context = parser.rpcStmt();

        var rpcNode = (RpcNode)_visitor.Visit(context);

        rpcNode.Input!.DataDefinitions.Should().HaveCount(1);
        ((LeafNode)rpcNode.Input.DataDefinitions[0]).Identifier.Should().Be("input-value");
        ((LeafNode)rpcNode.Input.DataDefinitions[0]).Type.Identifier.Should().Be("string");
        
        rpcNode.Output!.DataDefinitions.Should().HaveCount(1);
        ((LeafNode)rpcNode.Output.DataDefinitions[0]).Identifier.Should().Be("output-value");
        ((LeafNode)rpcNode.Output.DataDefinitions[0]).Type.Identifier.Should().Be("uint32");
    }
}