namespace YangParser.Model;

public class DeviationNode: INode
{
    public string Argument { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public List<INode> Deviates { get; set; } = new();
}