namespace YangParser.Model;

public class DeviateAddNode: INode
{
    public bool? Mandatory { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public bool? Config { get; set; }
    public string? Units { get; set; }
    public List<string> UniqueConstraints { get; set; } = new();
    public List<string> Default { get; set; } = new();
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
}