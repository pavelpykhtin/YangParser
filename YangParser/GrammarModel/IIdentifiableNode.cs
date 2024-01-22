namespace YangParser.Model;

public interface IIdentifiableNode: INode
{
    string Identifier { get; set; }
}