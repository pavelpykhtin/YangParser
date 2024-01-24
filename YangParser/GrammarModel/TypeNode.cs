using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// References type with applied restrictions.
/// </summary>
public class TypeNode : IIdentifiableNode
{
    /// <summary>
    /// Identifier of the base type.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Set of restrictions on numerical types.
    /// </summary>
    public NumericRestrictionsNode? NumericRestrictions { get; set; }

    /// <summary>
    /// Set of restrictions on string types.
    /// </summary>
    public StringRestrictionsNode? StringRestrictions { get; set; }

    /// <summary>
    /// Set of restrictions on enumeration types.
    /// </summary>
    public EnumSpecificationNode? EnumSpecification { get; set; }

    /// <summary>
    /// Definition of a reference to identity defined in this module or imported module.
    /// </summary>
    public IdentityReferenceNode? IdentityReference { get; set; }

    /// <summary>
    /// Set of restrictions on bit masks.
    /// </summary>
    public BitsSpecificationNode? BitsSpecification { get; set; }

    /// <summary>
    /// Set of restrictions on binnary arrays.
    /// </summary>
    public BinarySpecificationNode? BinarySpecification { get; set; }

    /// <summary>
    /// Set of restrictions on union of types.
    /// </summary>
    public UnionSpecificationNode? UnionSpecification { get; set; }

    /// <summary>
    /// Path to the tree node which value is referenced.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Is the node referenced with the Path property must exist in the tree.
    /// </summary>
    public bool? RequireInstance { get; set; }
}