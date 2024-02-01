using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.DeviationStmt;

public class DeviationStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public DeviationStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviationStmt/data/deviation.yang");

        var context = parser.deviationStmt();

        var deviationNode = (DeviationNode)_visitor.Visit(context);

        deviationNode.Argument.Should().Be("dummy-argument");
        deviationNode.Reference.Should().Be("dummy reference");
        deviationNode.Description.Should().Be("dummy description");
    }

    [Fact]
    public void HandlesDeviateStatements()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("DeviationStmt/data/deviation-deviate.yang");
        
        var context = parser.deviationStmt();
        
        var deviationNode = (DeviationNode)_visitor.Visit(context);
        
        deviationNode.Deviates.Should().HaveCount(4);
        
        deviationNode.Deviates[0].Should().BeOfType<DeviateAddNode>()
            .Which.Units.Should().Be("seconds");
        deviationNode.Deviates[1].Should().BeOfType<DeviateDeleteNode>()
            .Which.Units.Should().Be("minutes");
        deviationNode.Deviates[2].Should().BeOfType<DeviateReplaceNode>()
            .Which.Units.Should().Be("hours");
        deviationNode.Deviates[3].Should().BeOfType<DeviateNotSupportedNode>();
    }
}