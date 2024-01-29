using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Describes relation to parent module.</summary>
public class BelongsToNode : IIdentifiableNode
{
    /// <summary>Identifier of the module to which submodule belongs to.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Prefix used for definitions of the parent module.</summary>
    public string Prefix { get; set; } = null!;
}