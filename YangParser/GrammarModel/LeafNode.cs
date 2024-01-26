using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defininition of a data node with value.</summary>
public class LeafNode : IIdentifiableNode
{
    /// <summary>Identifier.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Type of a data node.</summary>
    public TypeNode Type { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the data node.</summary>
    public Status Status { get; set; }

    /// <summary>Wether the data node is mandatory.</summary>
    public bool Mandatory { get; set; }

    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Default value of a data node.</summary>
    public string? Default { get; set; }

    /// <summary>Units of a data node.</summary>
    public string? Units { get; set; }

    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }
}