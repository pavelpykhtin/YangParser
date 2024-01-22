namespace YangParser.Model;

public class IdentityNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Reference { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<string> Base { get; set; } = new();
}