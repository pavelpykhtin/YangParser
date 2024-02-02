using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines type based on built-in type "binary".</summary>
public class BinarySpecificationNode : INode
{
    /// <summary>Length constraint.</summary>
    public LengthNode? Length { get; set; }
}