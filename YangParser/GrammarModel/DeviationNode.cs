namespace YangParser.Model;

public class DeviationNode: INode
{
    public string Argument { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    public List<INode> Deviates { get; set; } = new();
}