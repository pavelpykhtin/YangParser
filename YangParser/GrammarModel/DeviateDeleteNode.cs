using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines properties to delete from a data node.</summary>
public class DeviateDeleteNode : INode
{
    ///<summary>Deletes predicates from Must collection of a data node.</summary>
    public List<MustNode> Must { get; init; } = new();

    ///<summary>When set removes property Units from a data node. Value of property in a data node MUST be equal to this property value.</summary>
    public string? Units { get; set; }

    ///<summary>Deletes constraints from Unique collection of a data node.</summary>
    public List<string> UniqueConstraints { get; set; } = new();

    ///<summary>When set removes property Default from a data node. Value of property in a data node MUST be equal to this property value.</summary>
    public List<string> Default { get; set; } = new();
}