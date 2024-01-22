namespace YangParser.Model;

public class BelongsToNode : IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string Prefix { get; set; } = null!;
}