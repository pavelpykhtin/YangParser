namespace YangParser.Model;

public class MustNode: INode
{
    public string Condition { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
}