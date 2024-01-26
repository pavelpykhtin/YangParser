using YangParser.GrammarModel;

namespace YangParser.Model;

public class GroupingNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public List<INode> DataDefinitions { get; set; } = new();
    public List<NotificationNode> Notifications { get; set; } = new();
    public List<ActionNode> Actions { get; set; } = new();
}