namespace YangParser.Model;

public class IncludeNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    public DateOnly? RevisionDate { get; set; } = new();
}