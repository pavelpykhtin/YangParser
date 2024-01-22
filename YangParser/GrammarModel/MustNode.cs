namespace YangParser.Model;

public class MustNode: INode
{
    public string Condition { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
}