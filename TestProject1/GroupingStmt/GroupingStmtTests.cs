﻿using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using FluentAssertions;
using TestProject1.Helpers;
using YangParser;
using YangParser.GrammarModel;
using YangParser.Model;

namespace TestProject1.GroupingStmt;

public class GroupingStmtTests
{
    private readonly YangRfcVisitor _visitor;

    public GroupingStmtTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Fact]
    public void HandlesCoreProperties()
    {
        var parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping.yang");

        var groupingStmt = parser.groupingStmt();
        
        var groupingNode = (GroupingNode)_visitor.Visit(groupingStmt);

        groupingNode.Identifier.Should().Be("login");
        groupingNode.Description.Should().Be("Message given at start of login session.");
        groupingNode.Reference.Should().Be("Dummy reference");
        groupingNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesNotifications()
    {
        var parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping-notification.yang");

        var groupingStmt = parser.groupingStmt();
        
        var groupingNode = (GroupingNode)_visitor.Visit(groupingStmt);
        
        groupingNode.Notifications.Should().HaveCount(1);
        groupingNode.Notifications[0].Identifier.Should().Be("if-damp-suppress");
    }
    
    [Fact]
    public void HandlesActions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping-action.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);

        groupingNode.Actions.Should().HaveCount(2);
        
        groupingNode.Actions[0].Identifier.Should().Be("action-a");
        
        groupingNode.Actions[1].Identifier.Should().Be("action-b");
    }

    [Fact]
    public void HandlesTypedefs()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping-typedef.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);

        groupingNode.Typedefs.Should().HaveCount(2);
        groupingNode.Typedefs[0].Identifier.Should().Be("percent");
        
        groupingNode.Typedefs[1].Identifier.Should().Be("minute");
    }

    [Fact]
    public void HandlesNestedGroupings()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping-grouping.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);

        groupingNode.Groupings.Should().HaveCount(2);
        groupingNode.Groupings[0].Identifier.Should().Be("nested1");
        groupingNode.Groupings[0].Description.Should().Be("nested description 1");
        
        groupingNode.Groupings[1].Identifier.Should().Be("nested2");
        groupingNode.Groupings[1].Description.Should().Be("nested description 2");
    }
    
    [Fact]
    public void HandlesDataDefinitions()
    {
        YangRfcParser parser = ParserHelpers.CreateParser("GroupingStmt/data/grouping-datadef.yang");

        var context = parser.groupingStmt();

        var groupingNode = (GroupingNode)_visitor.Visit(context);
        var containerNode = (ContainerNode)groupingNode.DataDefinitions[0];
        var leafNode = (LeafNode)groupingNode.DataDefinitions[1];
        var leafListNode = (LeafListNode)groupingNode.DataDefinitions[2];
        var listNode = (ListNode)groupingNode.DataDefinitions[3];
        var choiceNode = (ChoiceNode)groupingNode.DataDefinitions[4];
        var anyDataNode = (AnyDataNode)groupingNode.DataDefinitions[5];
        var anyXmlNode = (AnyXmlNode)groupingNode.DataDefinitions[6];
        var usesNode = (UsesNode)groupingNode.DataDefinitions[7];

        groupingNode.DataDefinitions.Should().HaveCount(8);
        
        containerNode.Identifier.Should().Be("nested-container");
        containerNode.Description.Should().Be("container description");
        
        leafNode.Identifier.Should().Be("nested-leaf");
        leafNode.Description.Should().Be("leaf description");
        
        leafListNode.Identifier.Should().Be("nested-leaf-list");
        leafListNode.Description.Should().Be("leaf-list description");
        
        listNode.Identifier.Should().Be("nested-list");
        listNode.Description.Should().Be("list description");
        
        choiceNode.Identifier.Should().Be("nested-choice");
        choiceNode.Description.Should().Be("choice description");
        
        anyDataNode.Identifier.Should().Be("nested-anydata");
        anyDataNode.Description.Should().Be("anydata description");
        
        anyXmlNode.Identifier.Should().Be("nested-anyxml");
        anyXmlNode.Description.Should().Be("anyxml description");
        
        usesNode.Identifier.Should().Be("nested-uses");
        usesNode.Description.Should().Be("uses description");
    }
}