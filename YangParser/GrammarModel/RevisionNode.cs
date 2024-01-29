using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines a revision of the module.</summary>
public class RevisionNode : INode
{
    /// <summary>Date of the revision.</summary>
    public DateOnly Date { get; set; }

    /// <summary>Description of changes.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}