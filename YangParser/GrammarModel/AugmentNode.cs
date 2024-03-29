﻿using YangParser.Model;

namespace YangParser.GrammarModel;

/// <summary>Defines augmentation for existing data node.</summary>
public class AugmentNode : INode
{
    /// <summary>Schema node identifier of a data node to augment.</summary>
    public string Argument { get; set; } = null!;

    /// <summary>Description.</summary>
    public string? Description { get; set; }

    /// <summary>Human readable reference for external document.</summary>
    public string? Reference { get; set; }

    /// <summary>Status of a data node.</summary>
    public Status Status { get; set; }

    /// <summary>Collection of predicates which are used to conditionaly ignore/remove data this data node.</summary>
    public WhenNode? When { get; set; }

    /// <summary>Collection of features which must be supported on the device in order to support these data node.</summary>
    public List<string> IfFeatures { get; set; } = new();

    ///<summary>Collection of data definitions to add to the data node.</summary>
    public List<INode> DataDefinitions { get; set; } = new();

    ///<summary>Collection of notifications to add to the data node.</summary>
    public List<NotificationNode> Notifications { get; set; } = new();

    ///<summary>Collection of actions to add to the data node.</summary>
    public List<ActionNode> Actions { get; set; } = new();

    ///<summary>Collection of cases to add to the data node.</summary>
    public List<CaseNode> Cases { get; set; } = new();
}