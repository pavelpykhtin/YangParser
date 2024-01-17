namespace YangParser.Model;

public class RevisionNode: INode
{
    public DateOnly Date { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
}