using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Contains a collection of definitions.
/// </summary>
public class ModuleNode : IIdentifiableNode
{
    /// <summary>
    /// Identifier of the module.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Namesapce of the module.
    /// </summary>
    public string Namespace { get; set; } = null!;

    /// <summary>
    /// Prefix used for definitions of module.
    /// </summary>
    public string Prefix { get; set; } = null!;

    /// <summary>
    /// Version of yang language.
    /// </summary>
    public string? YangVersion { get; set; }

    /// <summary>
    /// Party responsible for this module. 
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Contact information of the party responsible for the module.
    /// </summary>
    public string? Contact { get; set; }

    /// <summary>
    /// Description of the module.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Human readable link to external document.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Definitions of module.
    /// </summary>
    public List<INode> Body { get; set; } = new();

    /// <summary>
    /// History of changes in module.
    /// </summary>
    public List<RevisionNode> Revisions { get; set; } = new();

    /// <summary>
    /// List of inclueded submodules. Property BelongsTo of the included submodule must contain contain identifier of this module. 
    /// </summary>
    public List<IncludeNode> Includes { get; set; } = new();

    /// <summary>
    /// List of imported definitions from other modules or submodules.
    /// </summary>
    public List<ImportNode> Imports { get; set; } = new();
}