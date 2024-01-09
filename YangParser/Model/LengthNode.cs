namespace YangParser.Model;

public class LengthNode : INode
{
    public string Value { get; set; } = null!;
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    public string? Description {get;set;}
    public string? Reference {get;set;}
}