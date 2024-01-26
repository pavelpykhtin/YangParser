namespace YangParser.Model;

public class RevisionNode: INode
{
    public DateOnly Date { get; set; }
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}