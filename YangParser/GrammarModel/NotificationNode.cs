using YangParser.Model;

namespace YangParser.GrammarModel;

///<summary>Defines a notifcation.</summary>
public class NotificationNode : IIdentifiableNode
{
    /// <summary>Identifier of the notification.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to validate this data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Collection of type definitions.</summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>Collection of grouping definitions.</summary>
    public List<GroupingNode> Groupings { get; set; } = new();

    /// <summary>Collection of data definitions.</summary>
    public List<INode> DataDefinitions { get; set; } = new();
}