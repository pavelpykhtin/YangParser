namespace YangParser.Model;

public class UsesNode: INode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<RefineNode> Refine { get; set; } = new();
    public List<AugmentNode> Augment { get; set; } = new();
}