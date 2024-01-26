namespace YangParser.Model;

public class RefineNode: INode
{
    public string Argument { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }
    public string? Presence { get; set; }
    public bool Mandatory { get; set; }
    public int? MaxElements { get; set; }
    public int? MinElements { get; set; }
    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();
    public List<string> Default { get; set; } = new();
    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();
}