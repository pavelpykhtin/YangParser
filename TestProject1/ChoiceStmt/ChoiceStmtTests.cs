using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.Model;

namespace TestProject1.ChoiceStmt;

public class ChoiceStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public ChoiceStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ChoiceStmt/data/choice.yang");

        var context = parser.choiceStmt();

        var choiceNode = (ChoiceNode)_visitor.Visit(context);

        choiceNode.Identifier.Should().Be("context-engine-id");
        choiceNode.Mandatory.Should().BeTrue();
        choiceNode.Default.Should().Be("1.2.3.4");
        choiceNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\n             Applications.\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        choiceNode.Description.Should().Be("Dummy description");
        choiceNode.Config.Should().BeTrue();
        choiceNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesIfFeatures()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ChoiceStmt/data/choice-if.yang");

        var context = parser.choiceStmt();

        var choiceNode = (ChoiceNode)_visitor.Visit(context);
        
        choiceNode.IfFeatures.Should().HaveCount(1);
        choiceNode.IfFeatures[0].Should().Be("ssh");
    }

    [Fact]
    public void HandlesWhenStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ChoiceStmt/data/choice-when.yang");

        var context = parser.choiceStmt();

        var choiceNode = (ChoiceNode)_visitor.Visit(context);
        
        choiceNode.When!.Condition.Should().Be("../mode='ipv4-ipv6-address'");
        choiceNode.When!.Description.Should().Be("Dummy description");
        choiceNode.When!.Reference.Should().Be("Dummy reference");
    }

    [Fact]
    public void HandlesShortCases()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ChoiceStmt/data/choice-short-case.yang");
        
        var context = parser.choiceStmt();
        
        var choiceNode = (ChoiceNode)_visitor.Visit(context);
        
        choiceNode.ShortCases.Should().HaveCount(7);

        choiceNode.ShortCases[0].Should().BeOfType<ChoiceNode>()
            .Which.Identifier.Should().Be("dummy-choice");
        choiceNode.ShortCases[1].Should().BeOfType<ContainerNode>()
            .Which.Identifier.Should().Be("dummy-container");
        choiceNode.ShortCases[2].Should().BeOfType<LeafNode>()
            .Which.Identifier.Should().Be("dummy-leaf");
        choiceNode.ShortCases[3].Should().BeOfType<LeafListNode>()
            .Which.Identifier.Should().Be("dummy-leaf-list");
        choiceNode.ShortCases[4].Should().BeOfType<ListNode>()
            .Which.Identifier.Should().Be("dummy-list");
        choiceNode.ShortCases[5].Should().BeOfType<AnyDataNode>()
            .Which.Identifier.Should().Be("dummy-anydata");
        choiceNode.ShortCases[6].Should().BeOfType<AnyXmlNode>()
            .Which.Identifier.Should().Be("dummy-anyxml");
    }
    
    [Fact]
    public void HandlesCaseStatement()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("ChoiceStmt/data/choice-case.yang");

        var context = parser.choiceStmt();

        var choiceNode = (ChoiceNode)_visitor.Visit(context);

        choiceNode.Cases.Should().HaveCount(2);
        
        choiceNode.Cases[0].Identifier.Should().Be("case-1");
        choiceNode.Cases[0].Description.Should().Be("case description");
        
        choiceNode.Cases[1].Identifier.Should().Be("case-2");
    }
}