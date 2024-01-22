namespace YangParser.Model;

public class WhenNode: INode
{
    public string Condition { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
}