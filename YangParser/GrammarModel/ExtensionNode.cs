using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of new statements within YANG language.</summary>
public class ExtensionNode : IIdentifiableNode
{
    /// <summary>Identifier of the new statement. Will be used as a keyword for the statement.</summary>
    public string Identifier { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Argument of the new statement.</summary>
    public ArgumentNode? Argument { get; set; }
}