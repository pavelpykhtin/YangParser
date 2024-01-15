using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.TypedefStmt;

public class TypedefStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public TypedefStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("TypedefStmt/data/typedef.yang");

        var context = parser.typedefStmt();

        var typedefNode = (TypedefNode)_visitor.Visit(context);

        typedefNode.Identifier.Should().Be("context-engine-id");
        typedefNode.Type.Identifier.Should().Be("snmp:engine-id");
        typedefNode.Default.Should().Be("1.2.3.4");
        typedefNode.Units.Should().Be("ipv4");
        typedefNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).");
        typedefNode.Description.Should().Be("Dummy description");
        typedefNode.Status.Should().Be(Status.Current);
    }
}