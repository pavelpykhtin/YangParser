namespace YangParser.Model;

public class NumericRestrictionsNode : INode
{
    public int? FractionDigits { get; set; }
    public string? Range { get; set; }
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    public string? Description {get;set;}
    public string? Reference {get;set;}
}