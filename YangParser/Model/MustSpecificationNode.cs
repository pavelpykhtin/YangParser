namespace YangParser.Model;

public class MustSpecificationNode: INode
{
    public List<MustNode> Statements { get; init; } = new();
}