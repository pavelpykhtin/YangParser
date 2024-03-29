﻿using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Definition of an enumeration member.</summary>
public class EnumSpecifiationMemberNode : INode
{
    /// <summary>Enumeration member key.</summary>
    public string Key { get; set; }

    /// <summary>Enumeration member value.</summary>
    public int? Value { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; init; } = new();

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }
}