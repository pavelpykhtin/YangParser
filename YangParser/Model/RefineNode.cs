namespace YangParser.Model;

public class RefineNode: INode
{
    public string Argument { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public bool? Config { get; set; }
    public string? Presence { get; set; }
    public bool Mandatory { get; set; }
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<string> Default { get; set; } = new();
    public MustSpecificationNode Must { get; init; } = new();
}