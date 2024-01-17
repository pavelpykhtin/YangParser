using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.ExtensionStmt;

public class ExtensionStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public ExtensionStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("ExtensionStmt/data/extension.yang");

        var context = parser.extensionStmt();

        var extensionNode = (ExtensionNode)_visitor.Visit(context);

        extensionNode.Identifier.Should().Be("context-engine-id");
        extensionNode.Description.Should().Be("Dummy description");
        extensionNode.Reference.Should().Be("Dummy reference");
        extensionNode.Status.Should().Be(Status.Obsolete);
    }

    [Fact]
    public void HandlesArgument()
    {
        var parser = ParserHelpers.CreateParser("ExtensionStmt/data/extension-argument.yang");

        var context = parser.extensionStmt();

        var extensionNode = (ExtensionNode)_visitor.Visit(context);

        extensionNode.Argument!.Identifier.Should().Be("dummy-argument");
        extensionNode.Argument.YinElement.Should().Be(true);
    }
}