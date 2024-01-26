namespace YangParser.Model;

public class LengthNode : INode
{
    public string Value { get; set; } = null!;
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    /// <summary>Description.</summary>
    public string? Description {get;set;}
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference {get;set;}
}