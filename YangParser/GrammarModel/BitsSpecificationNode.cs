using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of a type based on built-in type "bits".</summary>
public class BitsSpecificationNode : INode
{
    /// <summary>Collection of a bit definitions.</summary>
    public List<BitSpecificationNode> Bits { get; set; } = new();
}