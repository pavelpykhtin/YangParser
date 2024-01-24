using YangParser.GrammarModel;

namespace YangParser.Model;

public class AugmentNode: INode
{
    public string Argument { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public WhenNode? When { get; set; }
    public List<string> IfFeatures { get; set; } = new();
    public List<INode> DataDefinitions { get; set; } = new();
    public List<NotificationNode> Notifications { get; set; } = new();
    public List<ActionNode> Actions { get; set; } = new();
    public List<CaseNode> Cases { get; set; } = new();
}