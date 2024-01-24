using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Definition of a collection of compound objects.
/// </summary>
public class ListNode : IIdentifiableNode
{
    /// <summary>Identifier of the list.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the list definition.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Whether these data are part of a device configuration or a device state.</summary>
    public bool? Config { get; set; }

    /// <summary>Minimum number of elements in collection.</summary>
    public int? MinElements { get; set; }

    /// <summary>Maximum number of elements in collection.</summary>
    public int? MaxElements { get; set; }

    /// <summary>Ordering policy.</summary>
    public OrderedBy? OrderedBy { get; set; }

    /// <summary>Collection of type definitions.</summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>List of reusable model block defined in this scope.</summary>
    public List<GroupingNode> Groupings { get; set; } = new();

    /// <summary>Collection of elements describing data structure.</summary>
    public List<INode> DataDefinitions { get; set; } = new();

    /// <summary>Collection of identifiers of nested leaf data nodes.</summary>
    public List<string> Keys { get; set; } = new();

    /// <summary>Collection of notifications.</summary>
    public List<NotificationNode> Notifications { get; set; } = new();

    /// <summary>Collection of actions associated with the collection instance.</summary>
    public List<ActionNode> Actions { get; set; } = new();

    /// <summary>Collection of identifiers of nested leaf data nodes which values are used in uniqueness constraint.</summary>
    public List<string> UniqueConstraints { get; set; } = new();
}