namespace YangParser.Model;

public class LeafListNode: INode
{
    public string Identifier { get; set; } = null!;
    public TypeNode Type { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public MustSpecificationNode Must { get; init; } = new();
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public string? Default { get; set; }
    public string? Units { get; set; }
    public bool? Config { get; set; }
    public int? MinElements { get; set; }
    public int? MaxElements { get; set; }
    public OrderedBy? OrderedBy { get; set; }
}