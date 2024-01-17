﻿namespace YangParser.Model;

public class InputNode: INode
{
    public MustSpecificationNode Must { get; init; } = new();
    public List<TypedefNode> Typedefs { get; set; } = new();
    public List<INode> DataDefinitions { get; init; } = new();
    public List<GroupingNode> Groupings { get; set; } = new();
}