namespace YangParser.Model;

public class EnumSpecificationNode: INode
{
    public List<EnumSpecifiationMemberNode> Members { get; init; } = new();
}