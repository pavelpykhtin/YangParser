using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.ImportStmt;

public class ImportStmtTests
{
    private const string Folder = "ImportStmt";
    private const string Prefix = "import";
    private readonly YangRfcVisitor _visitor;

    public ImportStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}.yang");

        var importStmt = parser.importStmt();
        
        var importNode = (ImportNode)_visitor.Visit(importStmt);

        importNode.Identifier.Should().Be("dummy-import");
        importNode.Description.Should().Be("Dummy description");
        importNode.Reference.Should().Be("Dummy reference");
        importNode.RevisionDate.Should().Be(new DateOnly(2024, 01, 19));
    }
}