using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Definition of a choise alternative.
/// </summary>
public class CaseNode : IIdentifiableNode
{
    /// <summary>Identifier of a choice case.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of the data node.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Collection of elements describing nested data nodes.</summary>
    public List<INode> DataDefinitions { get; init; } = new();
}