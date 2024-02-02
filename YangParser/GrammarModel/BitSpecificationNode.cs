using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of a bit in a bit set.</summary>
public class BitSpecificationNode : INode
{
    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; init; } = new();

    /// <summary>Position of a bit in a bit set.</summary>
    public int? Position { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}