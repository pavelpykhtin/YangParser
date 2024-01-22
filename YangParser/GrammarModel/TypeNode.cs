namespace YangParser.Model;

public class TypeNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public NumericRestrictionsNode? NumericRestrictions { get; set; }
    public StringRestrictionsNode? StringRestrictions { get; set; }
    public EnumSpecificationNode? EnumSpecification { get; set; }
    public IdentityReferenceNode? IdentityReference { get; set; }
    public BitsSpecificationNode? BitsSpecification { get; set; }
    public BinarySpecificationNode? BinarySpecification { get; set; }
    public UnionSpecificationNode? UnionSpecification { get; set; }

    public string? Path { get; set; }
    public bool? RequireInstance { get; set; }
}