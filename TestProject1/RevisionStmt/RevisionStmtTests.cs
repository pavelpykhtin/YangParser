using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.RevisionStmt;

public class RevisionStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public RevisionStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("RevisionStmt/data/revision.yang");

        var context = parser.revisionStmt();

        var revisionNode = (RevisionNode)_visitor.Visit(context);

        revisionNode.Date.Should().Be(new DateOnly(2024, 01, 17));
        revisionNode.Description.Should().Be("dummy description");
        revisionNode.Reference.Should().Be("dummy reference");
    }
}