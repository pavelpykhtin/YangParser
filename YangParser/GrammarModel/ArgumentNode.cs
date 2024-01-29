using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of an argument of the extension.</summary>
public class ArgumentNode : IIdentifiableNode
{
    /// <summary>Identifier of the argument.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Whether the argument is mapped to an XML element in YIN or to an XML attribute.</summary>
    public bool? YinElement { get; set; }
}