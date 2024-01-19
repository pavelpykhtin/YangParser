namespace YangParser.Model;

public class DeviateReplaceNode: INode
{
    public bool? Mandatory { get; set; }
    public bool? Config { get; set; }
    public string? Units { get; set; }
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
    public TypeNode? Type { get; set; }
    public List<string> Default { get; set; } = new();
}