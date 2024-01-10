using Antlr4.Runtime;
using FluentAssertions;
using YangParser;
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
        var parser = CreateParser("ContainerStmt/data/container.yang");

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
        YangRfcParser parser = CreateParser("ContainerStmt/data/container-if.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);
        
        containerNode.IfFeatures.Should().HaveCount(1);
        containerNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = CreateParser("ContainerStmt/data/container-when.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);
        
        containerNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        containerNode.When!.Description.Should().Be("Dummy description");
        containerNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesMustStatements()
    {
        YangRfcParser parser = CreateParser("ContainerStmt/data/container-must.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Must.Statements.Should().HaveCount(2);
        containerNode.Must.Statements[0].Condition.Should().Be("be available");
        containerNode.Must.Statements[0].Description.Should().Be("Dummy description");
        containerNode.Must.Statements[0].Reference.Should().Be("Dummy reference");
        containerNode.Must.Statements[0].ErrorMessage.Should().Be("Dummy error message");
        containerNode.Must.Statements[0].ErrorAppTag.Should().Be("Dummy error app tag");

        containerNode.Must.Statements[1].Condition.Should().Be("be enabled");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = CreateParser("ContainerStmt/data/container-typedef.yang");

        var context = parser.containerStmt();

        var containerNode = (ContainerNode)_visitor.Visit(context);

        containerNode.Typedefs.Should().HaveCount(2);
        containerNode.Typedefs[0].Identifier.Should().Be("percent");
        
        containerNode.Typedefs[1].Identifier.Should().Be("minute");
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