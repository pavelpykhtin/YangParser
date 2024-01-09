namespace YangParser.Model;

public class PatternNode : INode
{
    public string Value { get; set; } = null!;
    public string? ErrorMessage {get;set;}
    public string? ErrorAppTag {get;set;}
    public string? Description {get;set;}
    public string? Reference {get;set;}
    public bool InvertMatch => Modifier == PatternModifier.InvertMatch;
    public PatternModifier Modifier { get; set; } = PatternModifier.None;
}