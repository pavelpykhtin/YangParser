namespace YangParser.Model;

public class ModuleNode : INode
{
    public string Identifier { get; set; }
    public List<INode> Body { get; set; } = new();
}