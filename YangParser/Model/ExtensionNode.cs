namespace YangParser.Model;

public class ExtensionNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public ArgumentNode? Argument { get; set; }
}