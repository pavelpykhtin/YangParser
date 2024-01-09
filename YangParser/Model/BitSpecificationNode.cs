namespace YangParser.Model;

public class BitSpecificationNode: INode
{
    public List<string> IfFeatures { get; init; } = new();
    public int? Position { get; set; }
    public Status Status { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
}