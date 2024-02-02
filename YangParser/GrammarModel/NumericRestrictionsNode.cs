using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of a restrictions being applied to a numeric type.</summary>
public class NumericRestrictionsNode : INode
{
    /// <summary>Number of fractional digits on value.</summary>
    public int? FractionDigits { get; set; }

    /// <summary>Restrictions on a valid range.</summary>
    public string? Range { get; set; }

    /// <summary>Error message to show when value is not valid.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Value which will be passed as error-app-tag to rpc-error node if any constraint is failed.</summary>
    public string? ErrorAppTag { get; set; }

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}