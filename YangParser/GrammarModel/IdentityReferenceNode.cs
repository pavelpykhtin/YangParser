using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines a reference to an existing identity.</summary>
public class IdentityReferenceNode : INode
{
    ///<summary>Collection of base types from which referenced identitie's type must be derived.</summary>
    public List<string> BaseTypes { get; init; } = new();
}