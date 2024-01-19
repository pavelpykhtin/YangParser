using Antlr4.Runtime;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.IncludeStmt;

public class IncludeStmtTests
{
    private const string Folder = "IncludeStmt";
    private const string Prefix = "include";
    private readonly YangRfcVisitor _visitor;

    public IncludeStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser($"{Folder}/data/{Prefix}.yang");

        var includeStmt = parser.includeStmt();
        
        var includeNode = (IncludeNode)_visitor.Visit(includeStmt);

        includeNode.Identifier.Should().Be("dummy-include");
        includeNode.Description.Should().Be("Dummy description");
        includeNode.Reference.Should().Be("Dummy reference");
        includeNode.RevisionDate.Should().Be(new DateOnly(2024, 01, 19));
    }
}