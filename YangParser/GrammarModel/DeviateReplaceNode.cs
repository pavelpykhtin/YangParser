using YangParser.GrammarModel;

namespace YangParser.Model;

public class DeviateReplaceNode: INode
{
    /// <summary>Wether the data node is mandatory for data tree in order to be valid.</summary>
    public bool? Mandatory { get; set; }
    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }
    public string? Units { get; set; }
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
    public TypeNode? Type { get; set; }
    public List<string> Default { get; set; } = new();
}