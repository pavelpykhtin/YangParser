using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Definition of an output parameter.
/// </summary>
public class OutputNode : INode
{
    /// <summary>Collection of predicates which are used to validate data tree.</summary>
    public List<MustNode> Must { get; init; } = new();

    /// <summary>Collection of type definitions.</summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>Collection of elements describing data structure.</summary>
    public List<INode> DataDefinitions { get; init; } = new();

    /// <summary>List of reusable model block defined in this scope.</summary>
    public List<GroupingNode> Groupings { get; set; } = new();
}