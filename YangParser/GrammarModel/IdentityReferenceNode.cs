namespace YangParser.Model;

public class IdentityReferenceNode: INode
{
    public List<string> References { get; init; } = new();
}