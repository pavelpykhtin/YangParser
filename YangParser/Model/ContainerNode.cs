namespace YangParser.Model;

public class ContainerNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public bool? Config { get; set; }
    public string? Presence { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<NotificationNode> Notifications { get; set; } = new();
    public List<INode> DataDefinitions { get; init; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public List<ActionNode> Actions { get; set; } = new();
}