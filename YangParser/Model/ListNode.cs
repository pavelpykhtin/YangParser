namespace YangParser.Model;

public class ListNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public bool? Config { get; set; }
    public int? MinElements { get; set; }
    public int? MaxElements { get; set; }
    public OrderedBy? OrderedBy { get; set; }
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public List<INode> DataDefinitions { get; set; } = new();
    public List<string> Keys { get; set; } = new();
    public List<NotificationNode> Notifications { get; set; } = new();
    public List<ActionNode> Actions { get; set; } = new();
    public List<string> UniqueConstraints { get; set; } = new();
}