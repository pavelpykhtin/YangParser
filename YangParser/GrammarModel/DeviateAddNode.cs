using YangParser.Model;

namespace YangParser.GrammarModel;

///<summary>Defines properties to add to a data node.</summary>
public class DeviateAddNode : INode
{
    ///<summary>When set adds Mandatory property.</summary>
    public bool? Mandatory { get; set; }

    ///<summary>Adds predicates to Must collection of a data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    ///<summary>When set adds Config property.</summary>
    public bool? Config { get; set; }

    ///<summary>When set adds Units property.</summary>
    public string? Units { get; set; }

    ///<summary>Adds constraints to Unique collection of a data node.</summary>
    public List<string> UniqueConstraints { get; set; } = new();

    ///<summary>When set adds Default property.</summary>
    public List<string> Default { get; set; } = new();

    ///<summary>When set adds MaxElements property.</summary>
    public int? MaxElements { get; set; }

    ///<summary>When set adds MinElements property.</summary>
    public int? MinElements { get; set; }
}