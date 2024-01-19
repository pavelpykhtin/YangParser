namespace YangParser.Model;

public class SubmoduleNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string YangVersion { get; set; } = null!;
    public string? Organization { get; set; }
    public string? Contact { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public List<INode> Body { get; set; } = new();
    public List<RevisionNode> Revisions { get; set; } = new();
    public List<IncludeNode> Includes { get; set; } = new();
    public List<ImportNode> Imports { get; set; } = new();
}