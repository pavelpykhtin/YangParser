namespace YangParser.Model;

public class DeviateAddNode: INode
{
    /// <summary>Wether the data node is mandatory for data tree in order to be valid.</summary>
    public bool? Mandatory { get; set; }
    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();
    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }
    public string? Units { get; set; }
    public List<string> UniqueConstraints { get; set; } = new();
    public List<string> Default { get; set; } = new();
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
}