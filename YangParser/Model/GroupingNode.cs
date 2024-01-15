namespace YangParser.Model;

public class GroupingNode: INode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public List<INode> DataDefinitions { get; set; } = new();
    public List<NotificationNode> Notifications { get; set; } = new();
}