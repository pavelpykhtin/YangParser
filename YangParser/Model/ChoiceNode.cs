namespace YangParser.Model;

public class ChoiceNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public bool Mandatory { get; set; }
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public string? Default { get; set; }
    public bool? Config { get; set; }
    public List<INode> ShortCases { get; set; } = new();
    public List<CaseNode> Cases { get; set; } = new();
}