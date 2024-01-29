using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Imports definitions from another module.</summary>
public class ImportNode : IIdentifiableNode
{
    /// <summary>Name of the module to import.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Prefix used for definitions of module. Prefix is scoped to importing module.</summary>
    public string Prefix { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Specifies the revision of the module to import.</summary>
    public DateOnly? RevisionDate { get; set; } = new();
}