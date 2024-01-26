namespace YangParser.Model;

public class FeatureNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    /// <summary>Description.</summary>
    public string? Description { get; set; }
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }
    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();
}