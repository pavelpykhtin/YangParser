using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Helper class used to wrap string value into INode container. Introduced to match Visitor API.
/// </summary>
public class StringNode : INode
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Value to wrap.</param>
    public StringNode(string? value) => Value = value;

    /// <summary>
    /// Value.
    /// </summary>
    public string? Value { get; set; }
}