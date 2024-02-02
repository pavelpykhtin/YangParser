using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of an enumeration type.</summary>
public class EnumSpecificationNode : INode
{
    /// <summary>Collection of enumeration members.</summary>
    public List<EnumSpecifiationMemberNode> Members { get; init; } = new();
}