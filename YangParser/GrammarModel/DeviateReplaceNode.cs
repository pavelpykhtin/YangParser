using YangParser.Model;

namespace YangParser.GrammarModel;

///<summary>Defines properties to change in a data node.</summary>
public class DeviateReplaceNode : INode
{
    /// <summary>New Mandatory value.</summary>
    public bool? Mandatory { get; set; }

    /// <summary>New Config value.</summary>
    public bool? Config { get; set; }

    ///<summary>New Units value.</summary>
    public string? Units { get; set; }

    ///<summary>New MaxElements value.</summary>
    public int? MaxElements { get; set; }

    ///<summary>New MinElements value.</summary>
    public int? MinElements { get; set; }

    /// <summary>New Type value.</summary>
    public TypeNode? Type { get; set; }

    /// <summary>New Default value.</summary>
    public List<string> Default { get; set; } = new();
}