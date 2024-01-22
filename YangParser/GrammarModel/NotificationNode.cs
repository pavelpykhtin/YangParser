﻿namespace YangParser.Model;

public class NotificationNode: IIdentifiableNode
{
    public string Identifier { get; set; } = null!;
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public Status Status { get; set; }
    public List<MustNode> Must { get; init; } = new();
    public List<string> IfFeatures { get; set; } = new();
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
    public List<INode> DataDefinitions { get; set; } = new();
}