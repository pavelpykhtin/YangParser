namespace YangParser.Model;

public class IncludeNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public DateOnly? RevisionDate { get; set; } = new();
}