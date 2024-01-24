using YangParser.GrammarModel;

namespace YangParser.Model;

public class LeafNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public TypeNode Type { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public bool Mandatory { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public string? Default { get; set; }
    public string? Units { get; set; }
    public bool? Config { get; set; }
}