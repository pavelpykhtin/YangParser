namespace YangParser.Model;

public class ModuleNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string Namespace { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public string YangVersion { get; set; } = null!;
    public string? Organization { get; set; }
    public string? Contact { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public List<INode> Body { get; set; } = new();
    public List<RevisionNode> Revisions { get; set; } = new();
}