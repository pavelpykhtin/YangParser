using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines constraint on a length of a value.</summary>
public class LengthNode : INode
{
    ///<summary>Exprission that descibes range of valid length values.</summary>
    public string Value { get; set; } = null!;

    /// <summary>Error message to show when length is not valid.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Value which will be passed as error-app-tag to rpc-error node if constraint is failed.</summary>
    public string? ErrorAppTag { get; set; }

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}