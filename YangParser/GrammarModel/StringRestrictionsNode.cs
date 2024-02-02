using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of constraint on a string value.</summary>
public class StringRestrictionsNode : INode
{
    /// <summary>Length constraint.</summary>
    public LengthNode? Length { get; set; }

    /// <summary>List of patterns allowed for values.</summary>
    public List<PatternNode> Patterns { get; init; } = new();
}