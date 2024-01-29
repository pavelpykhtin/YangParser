using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of override rule for a grouping.</summary>
public class RefineNode : INode
{
    /// <summary>Identifier of a data node in grouping to override.</summary>
    public string Argument { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>New value of a data node's Config property.</summary>
    public bool? Config { get; set; }

    /// <summary>New value of a data node's Presence property.</summary>
    public string? Presence { get; set; }

    /// <summary>New value of a data node's Mandatory property.</summary>
    public bool Mandatory { get; set; }

    /// <summary>New value of a data node's MaxElements property.</summary>
    public int? MaxElements { get; set; }

    /// <summary>New value of a data node's MinElements property.</summary>
    public int? MinElements { get; set; }

    /// <summary>New collection of IfFeatures predicates of a data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>New value of a data node's Default property.</summary>
    public List<string> Default { get; set; } = new();

    /// <summary>New collection of Must predicates of a data node.</summary>
    public List<MustNode> Must { get; init; } = new();
}