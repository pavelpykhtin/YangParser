namespace YangParser.Model;

public class ArgumentNode: IIdentifiableNode
{
    public string Identifier { get; set; }
    public bool? YinElement { get; set; }
}