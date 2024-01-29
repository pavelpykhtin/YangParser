using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Reference to a grouping block.</summary>
public class UsesNode : INode
{
    /// <summary>Identifier of a grouping.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Collection of refinement rulles applied to the referenced grouping.</summary>
    public List<RefineNode> Refine { get; set; } = new();

    /// <summary>Collection of augmentation rules applied to the referenced grouping.</summary>
    public List<AugmentNode> Augment { get; set; } = new();
}