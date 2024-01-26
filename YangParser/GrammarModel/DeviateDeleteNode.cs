namespace YangParser.Model;

public class DeviateDeleteNode: INode
{
    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();
    public string? Units { get; set; }
    public List<string> UniqueConstraints { get; set; } = new();
    public List<string> Default { get; set; } = new();
}