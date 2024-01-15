using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using FluentAssertions;
using YangParser;
using YangParser.Model;

namespace TestProject1.InputStmt;

public class InputStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public InputStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = CreateParser("InputStmt/data/input-must.yang");

        var context = parser.inputStmt();

        var inputNode = (InputNode)_visitor.Visit(context);

        inputNode.Must.Statements.Should().HaveCount(2);
        inputNode.Must.Statements[0].Condition.Should().Be("be available");
        inputNode.Must.Statements[0].Description.Should().Be("Dummy description");
        inputNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        inputNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        inputNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        inputNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = CreateParser("InputStmt/data/input-typedef.yang");

        var context = parser.inputStmt();

        var inputNode = (InputNode)_visitor.Visit(context);

        inputNode.Typedefs.Should().HaveCount(2);
        inputNode.Typedefs[0].Identifier.Should().Be("percent");
        
        inputNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesGrouppings()
    {
        YangRfcParser parser = CreateParser("InputStmt/data/input-grouping.yang");

        var context = parser.inputStmt();

        var inputNode = (InputNode)_visitor.Visit(context);

        inputNode.Groupings.Should().HaveCount(2);
        
        inputNode.Groupings[0].Identifier.Should().Be("grouping-a");
        
        inputNode.Groupings[1].Identifier.Should().Be("grouping-b");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = CreateParser("InputStmt/data/input-datadef.yang");

        var context = parser.inputStmt();

        var inputNode = (InputNode)_visitor.Visit(context);
        var nestedContainerNode = (ContainerNode)inputNode.DataDefinitions[0];
        var leafNode = (LeafNode)inputNode.DataDefinitions[1];

        inputNode.DataDefinitions.Should().HaveCount(2);
        
        nestedContainerNode.Identifier.Should().Be("nested-container");
        nestedContainerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
    }

    private YangRfcParser CreateParser(string filePath)
    {
        using var input = File.OpenText(filePath);

        AntlrInputStream inputStream = new AntlrInputStream(input);
        YangRfcLexer lexer = new YangRfcLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YangRfcParser parser = new YangRfcParser(commonTokenStream);
        parser.Interpreter.PredictionMode = PredictionMode.LlExactAmbigDetection;

        return parser;
    }
}