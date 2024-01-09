namespace YangParser.Model;

public class EnumSpecifiationMemberNode: INode
{
    public string Key { get; set; }
    public int? Value { get; set; }
    public List<string> IfFeatures { get; init; } = new();
    public Status Status { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
}