namespace YangParser.Model;

public class DeviateDeleteNode: INode
{
    public List<MustNode> Must { get; init; } = new();
    public string? Units { get; set; }
    public List<string> UniqueConstraints { get; set; } = new();
    public List<string> Default { get; set; } = new();
}