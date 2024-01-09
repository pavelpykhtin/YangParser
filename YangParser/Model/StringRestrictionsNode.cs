namespace YangParser.Model;

public class StringRestrictionsNode : INode
{
    public LengthNode? Length { get; set; }
    public List<PatternNode> Patterns { get; init; } = new();
}