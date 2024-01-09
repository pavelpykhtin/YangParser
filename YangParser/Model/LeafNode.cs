namespace YangParser.Model;

public class LeafNode: INode
{
    public string Identifier { get; set; }
    public TypeNode Type { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public bool Mandatory { get; set; }
}

public class TypeNode: INode
{
    public string Identifier { get; set; }
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

public class UnionSpecificationNode: INode
{
    public List<TypeNode> Types { get; init; } = new();
}