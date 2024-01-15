namespace YangParser.Model;

public class ModuleNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public List<INode> Body { get; set; } = new();
}