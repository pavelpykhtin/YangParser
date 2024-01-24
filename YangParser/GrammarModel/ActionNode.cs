using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Action associated with container of list.
/// </summary>
public class ActionNode : IIdentifiableNode
{
    /// <summary>
    /// Name of the action.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Decription.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Human readable reference for external document.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Status of the action.
    /// </summary>    
    public Status Status { get; set; }

    /// <summary>
    /// List of features that server must support in order to use this action.
    /// </summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>
    /// List of type definitions.
    /// </summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>
    /// List of groupings.
    /// </summary>
    public List<GroupingNode> Groupings { get; set; } = new();

    /// <summary>
    /// Description of input argument.
    /// </summary>
    public InputNode? Input { get; set; }

    /// <summary>
    /// Description of output.
    /// </summary>
    public OutputNode? Output { get; set; }
}