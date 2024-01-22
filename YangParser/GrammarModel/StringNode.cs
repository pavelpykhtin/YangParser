namespace YangParser.Model;

public class StringNode : INode
{
    public StringNode(string? value) => Value = value;

    public string? Value { get; set; }
}