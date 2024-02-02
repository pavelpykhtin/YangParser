using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of a pattern which the value must follow.</summary>
public class PatternNode : INode
{
    /// <summary>Regular expression used to validate values of the string.</summary>
    public string Value { get; set; } = null!;

    /// <summary>Error message to show when the value does not match the pattern.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Value which will be passed as error-app-tag to rpc-error node if constraint is failed.</summary>
    public string? ErrorAppTag { get; set; }

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Wether the results of the pattern match should be inverted.</summary>
    public bool InvertMatch => Modifier == PatternModifier.InvertMatch;

    /// <summary>Match modifier.</summary>
    public PatternModifier Modifier { get; set; } = PatternModifier.None;
}