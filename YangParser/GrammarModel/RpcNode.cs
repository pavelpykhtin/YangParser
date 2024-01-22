namespace YangParser.Model;

public class RpcNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public InputNode? Input { get; set; }
    public OutputNode? Output { get; set; }
}