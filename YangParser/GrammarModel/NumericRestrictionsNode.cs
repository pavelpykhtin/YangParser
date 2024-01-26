namespace YangParser.Model;

public class NumericRestrictionsNode : INode
{
    public int? FractionDigits { get; set; }
    public string? Range { get; set; }
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    /// <summary>Description.</summary>
    public string? Description {get;set;}
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference {get;set;}
}