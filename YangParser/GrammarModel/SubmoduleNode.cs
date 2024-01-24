using YangParser.Model;

namespace YangParser.GrammarModel;

public class SubmoduleNode : IIdentifiableNode
{
    /// <summary>
    /// Identifier of the submodule.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Namesapce of the submodule.
    /// </summary>
    public string YangVersion { get; set; } = null!;

    /// <summary>
    /// Party responsible for this submodule.
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Contact information of the party responsible for the submodule.
    /// </summary>
    public string? Contact { get; set; }

    /// <summary>
    /// Description of the submodule.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Human readable reference for external document.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Collection of definitions.
    /// </summary>
    public List<INode> Body { get; set; } = new();

    /// <summary>
    /// History of changes of submodule.
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

    /// <summary>
    /// Identifier of the module to which submodule belongs to.
    /// </summary>
    public BelongsToNode BelongsTo { get; set; }
}