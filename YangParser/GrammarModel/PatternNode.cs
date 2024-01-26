namespace YangParser.Model;

public class PatternNode : INode
{
    public string Value { get; set; } = null!;
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    /// <summary>Description.</summary>
    public string? Description {get;set;}
    /// <summary>Human readable reference for external document.</summary>
    public string? Reference {get;set;}
    public bool InvertMatch => Modifier == PatternModifier.InvertMatch;
    public PatternModifier Modifier { get; set; } = PatternModifier.None;
}