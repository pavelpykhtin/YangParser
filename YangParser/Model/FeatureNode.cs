namespace YangParser.Model;

public class FeatureNode: INode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public List<string> IfFeatures { get; set; } = new();
}