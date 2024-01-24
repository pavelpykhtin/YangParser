using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Defines a new type that can be used localy, in submodules or modules that import it from this module.
/// </summary>
public class TypedefNode : IIdentifiableNode
{
    /// <summary>
    /// Identifier of the new type.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Definition of the base type from which this type is derived.
    /// </summary>
    public TypeNode Type { get; set; } = null!;

    /// <summary>
    /// Definition status.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Description of the type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Human readable reference for external document.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Default value of the type.
    /// </summary>
    public string? Default { get; set; }

    /// <summary>
    /// Textual definition of the units associated with the type.
    /// </summary>
    public string? Units { get; set; }
}