using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Collection of data nodes with value.</summary>
public class LeafListNode : IIdentifiableNode
{
    /// <summary>Identifier of the data nodes.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Type of values in data nodes.</summary>
    public TypeNode Type { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the data node.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Default value of the data node in collection.</summary>
    public string? Default { get; set; }

    /// <summary>Units of the data nodes value.</summary>
    public string? Units { get; set; }

    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }

    /// <summary>Minimum number of elements in collection.</summary>
    public int? MinElements { get; set; }

    /// <summary>Maximum number of elements in collection.</summary>
    public int? MaxElements { get; set; }

    /// <summary>Ordering policy.</summary>
    public OrderedBy? OrderedBy { get; set; }
}