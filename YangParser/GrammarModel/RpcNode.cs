using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>
/// Definition of RPC operation.
/// </summary>
public class RpcNode : IIdentifiableNode
{
    /// <summary>Identifier of the RPC operation.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Expression which describes availability of the data node depending on availability of feature implementaion on the device.</summary>
    public List<string> IfFeatures { get; set; } = new();

    /// <summary>Collection of typ definitions.</summary>
    public List<TypedefNode> Typedefs { get; set; } = new();

    /// <summary>Collection of reusable blocks defined in this scope.</summary>
    public List<GroupingNode> Groupings { get; set; } = new();

    /// <summary>Definition of input argument.</summary>
    public InputNode? Input { get; set; }

    /// <summary>Definition of output argument.</summary>
    public OutputNode? Output { get; set; }
}