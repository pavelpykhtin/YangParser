using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Imports definitions from a submodule.</summary>
public class IncludeNode : IIdentifiableNode
{
    /// <summary>Name of the submodule to include.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Specifies the revision of the submodule to include.</summary>
    public DateOnly? RevisionDate { get; set; } = new();
}