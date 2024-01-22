namespace YangParser.Model;

public class AnyDataNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public bool? Mandatory { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public bool? Config { get; set; }
}