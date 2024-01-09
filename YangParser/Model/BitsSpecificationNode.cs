namespace YangParser.Model;

public class BitsSpecificationNode: INode
{
    public List<BitSpecificationNode> Bits { get; set; } = new();
}