namespace YangParser.Model;

public class UnionSpecificationNode: INode
{
    public List<TypeNode> Types { get; init; } = new();
}