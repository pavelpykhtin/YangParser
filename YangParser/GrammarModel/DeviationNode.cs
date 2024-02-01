using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines deviation of the implementation from original definition.</summary>
public class DeviationNode : INode
{
    /// <summary>Abosolute schema node identifier.</summary>
    public string Argument { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Collection statements which describe deviations from the original definition.</summary>
    public List<INode> Deviates { get; set; } = new();
}