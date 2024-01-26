namespace YangParser.Model;

public class WhenNode: INode
{
    public string Condition { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}