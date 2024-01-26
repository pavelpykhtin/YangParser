using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of a compound data node.</summary>
public class ContainerNode : IIdentifiableNode
{
    /// <summary>Identifier of the data node.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the data node.</summary>
    public Status Status { get; set; }

    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }

    /// <summary>
    /// Whether the compound data node has own meaning (true) or just a bundle of nested elements (false).
    /// Non-presence data node are deleted if they do not have a nested elements.
    /// </summary>
    public string? Presence { get; set; }

    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Collection of type definitions.</summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>Collection of notifications.</summary>
    public List<NotificationNode> Notifications { get; set; } = new();

    /// <summary>Collection of elements describing nested data nodes.</summary>
    public List<INode> DataDefinitions { get; init; } = new();

    /// <summary>List of reusable model block defined in this scope.</summary>
    public List<GroupingNode> Groupings { get; set; } = new();

    /// <summary>Collection of actions associated with the collection instance.</summary>
    public List<ActionNode> Actions { get; set; } = new();
}