namespace YangParser.Model;

public class TypedefNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public TypeNode Type { get; set; } = null!;
    public Status Status { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public string? Default { get; set; }
    public string? Units { get; set; }
}