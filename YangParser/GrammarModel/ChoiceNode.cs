using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of conditional data section.</summary>
public class ChoiceNode : IIdentifiableNode
{
    /// <summary>Identifier of the definition.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the data node.</summary>
    public Status Status { get; set; }

    /// <summary>When True at least one child data node must be provided by exactly one case.</summary>
    public bool Mandatory { get; set; }

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Identifier of the default case statement which will be used if no child node were provided by any case definition.</summary>
    public string? Default { get; set; }

    /// <summary>Whether child data nodes are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }

    /// <summary>Choise alternatives in a short form.</summary>
    public List<INode> ShortCases { get; set; } = new();

    /// <summary>Choise alternatives in a detailed form.</summary>
    public List<CaseNode> Cases { get; set; } = new();
}