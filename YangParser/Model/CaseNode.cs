namespace YangParser.Model;

public class CaseNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<INode> DataDefinitions { get; init; } = new();
}