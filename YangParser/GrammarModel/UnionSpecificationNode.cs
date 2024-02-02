using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of union type.</summary>
public class UnionSpecificationNode : INode
{
    /// <summary>Collection of types allowed for the value.</summary>
    public List<TypeNode> Types { get; init; } = new();
}