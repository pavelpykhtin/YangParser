using YangParser.Extensions;
using YangParser.Model;

namespace YangParser;

public class YangRfcVisitor: YangRfcParserBaseVisitor<INode>
{
    public override INode VisitModuleStmt(YangRfcParser.ModuleStmtContext context) =>
        new ModuleNode
        {
            Identifier = context.identifier().GetText(),
            Body = context.bodyStmts().Map(ParseBodyStmts) ?? new List<INode>(),
        };

    public override INode VisitSubmoduleStmt(YangRfcParser.SubmoduleStmtContext context) =>
        new SubmoduleNode
        {
            Identifier = context.identifier().GetText()
        };

    public override INode VisitFeatureStmt(YangRfcParser.FeatureStmtContext context) =>
        new FeatureNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };

    public override INode VisitLeafStmt(YangRfcParser.LeafStmtContext context) =>
        new LeafNode
        {
            Identifier = context.identifier().GetText(),
            Type = (TypeNode)VisitTypeStmt(context.typeStmt().Single()),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Mandatory = context.mandatoryStmt().MapSingle(x => bool.Parse(x.mandatoryArgStr().GetText())),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Default = context.defaultStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(x => bool.Parse(x.configArgStr().GetText())),
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt())
        };

    public override INode VisitLeafListStmt(YangRfcParser.LeafListStmtContext context) =>
        new LeafListNode
        {
            Identifier = context.identifier().GetText(),
            Type = (TypeNode)VisitTypeStmt(context.typeStmt().Single()),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Default = context.defaultStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(x => bool.Parse(x.configArgStr().GetText())),
            MinElements = context.minElementsStmt().MapSingle(x => int.Parse(x.minValueArgStr().GetText())),
            MaxElements = context.maxElementsStmt().MapSingle(x => int.Parse(x.maxValueArgStr().GetText())),
            OrderedBy = context.orderedByStmt().MapSingle(ParseOrderedBy),
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt())
        };

    public override INode VisitListStmt(YangRfcParser.ListStmtContext context) =>
        new ListNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Config = context.configStmt().MapSingle(x => bool.Parse(x.configArgStr().GetText())),
            MinElements = context.minElementsStmt().MapSingle(x => int.Parse(x.minValueArgStr().GetText())),
            MaxElements = context.maxElementsStmt().MapSingle(x => int.Parse(x.maxValueArgStr().GetText())),
            OrderedBy = context.orderedByStmt().MapSingle(ParseOrderedBy),
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Keys = context.keyStmt()
                       .MapSingle(x => x.keyArgStr())
                       .Map(x => x.keyArg())
                       .Map(x => x.nodeIdentifier())
                       .Map(x => x.Select(i => i.GetText()).ToList()) 
                   ?? new(),
            Notifications = context.notificationStmt().Select(x => (NotificationNode)VisitNotificationStmt(x)).ToList(),
            Actions = context.actionStmt().Select(x => (ActionNode)VisitActionStmt(x)).ToList(),
            UniqueConstraints = context.uniqueStmt()
                .Select(x => (StringNode)VisitUniqueStmt(x))
                .Select(x => x.Value!)
                .ToList()
        };

    public override INode VisitContainerStmt(YangRfcParser.ContainerStmtContext context) =>
        new ContainerNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Config = context.configStmt().MapSingle(x => bool.Parse(x.configArgStr().GetText())),
            Presence = context.presenceStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Notifications = context.notificationStmt().Select(x => (NotificationNode)VisitNotificationStmt(x)).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Actions = context.actionStmt().Select(x => (ActionNode)VisitActionStmt(x)).ToList(),
        };

    public override INode VisitTypedefStmt(YangRfcParser.TypedefStmtContext context) =>
        new TypedefNode
        {
            Identifier = context.identifier().GetText(),
            Type = (TypeNode)VisitTypeStmt(context.typeStmt().Single()),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Default = context.defaultStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
        };

    public override INode VisitGroupingStmt(YangRfcParser.GroupingStmtContext context) =>
        new GroupingNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Notifications = context.notificationStmt().Select(x => (NotificationNode)VisitNotificationStmt(x)).ToList(),
            Actions = context.actionStmt().Select(x => (ActionNode)VisitActionStmt(x)).ToList(),
        };

    public override INode VisitNotificationStmt(YangRfcParser.NotificationStmtContext context)
    {
        return new NotificationNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList()
        };
    }

    public override INode VisitDataDefStmt(YangRfcParser.DataDefStmtContext context) =>
        context.containerStmt().Map(VisitContainerStmt)
        ?? context.leafStmt().Map(VisitLeafStmt)
        ?? context.leafListStmt().Map(VisitLeafListStmt)
        ?? context.listStmt().Map(VisitListStmt)
        ?? throw new NotImplementedException("Unknown data-definition type");

    public override INode VisitTypeStmt(YangRfcParser.TypeStmtContext context)
    {
        var body = context.typeBodyStmts();
        var leafrefContext = body?.leafrefSpecification();

        return new TypeNode
        {
            Identifier = context.identifierRefArgStr().GetText(),
            NumericRestrictions = body?.numericalRestrictions().Map(x => (NumericRestrictionsNode)VisitNumericalRestrictions(x)),
            StringRestrictions = body?.stringRestrictions().Map(x => (StringRestrictionsNode)VisitStringRestrictions(x)),
            EnumSpecification = body?.enumSpecification().Map(x => (EnumSpecificationNode)VisitEnumSpecification(x)),
            IdentityReference = body?.identityrefSpecification().Map(x => (IdentityReferenceNode)VisitIdentityrefSpecification(x)),
            BitsSpecification = body?.bitsSpecification().Map(x => (BitsSpecificationNode)VisitBitsSpecification(x)),
            BinarySpecification = body?.binarySpecification().Map(x => (BinarySpecificationNode)VisitBinarySpecification(x)),
            UnionSpecification = body?.unionSpecification().Map(x => (UnionSpecificationNode)VisitUnionSpecification(x)),
            Path = leafrefContext?.pathStmt().MapSingle(x => x.pathArgStr()).Map(x => VisitQuotedString(x.quotedString())).StringValue(),
            RequireInstance = leafrefContext?.requireInstanceStmt().MapSingle(x => (bool?)bool.Parse(x.requireInstanceArgStr().GetText())),
        };
    }

    public override INode VisitActionStmt(YangRfcParser.ActionStmtContext context) =>
        new ActionNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Input = context.inputStmt().MapSingle(x => (InputNode)VisitInputStmt(x)),
            Output = context.outputStmt().MapSingle(x => (OutputNode)VisitOutputStmt(x)),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };

    public override INode VisitRpcStmt(YangRfcParser.RpcStmtContext context) =>
        new RpcNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Input = context.inputStmt().MapSingle(x => (InputNode)VisitInputStmt(x)),
            Output = context.outputStmt().MapSingle(x => (OutputNode)VisitOutputStmt(x)),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };

    public override INode VisitStringRestrictions(YangRfcParser.StringRestrictionsContext context) =>
        new StringRestrictionsNode
        {
            Length = context.lengthStmt().MapSingle(x => (LengthNode)VisitLengthStmt(x)),
            Patterns = context.patternStmt().Select(x => (PatternNode)VisitPatternStmt(x)).ToList()
        };

    public override INode VisitNumericalRestrictions(YangRfcParser.NumericalRestrictionsContext context)
    {
        var rangeContext = context.rangeStmt().SingleOrDefault();
        return new NumericRestrictionsNode
        {
            FractionDigits = context.fractionDigitsStmt().MapSingle(x => int.Parse(x.fractionDigitsArgStr().GetText())),
            Range = rangeContext?.rangeArgStr().Map(x => VisitQuotedString(x.quotedString()))?.StringValue(),
            ErrorMessage = rangeContext?.errorMessageStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            ErrorAppTag = rangeContext?.errorAppTagStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Description = rangeContext?.descriptionStmt().MapSingle(x => (StringNode)VisitDescriptionStmt(x))?.Value,
            Reference = rangeContext?.referenceStmt().MapSingle(x => (StringNode)VisitReferenceStmt(x))?.Value
        };
    }

    public override INode VisitLengthStmt(YangRfcParser.LengthStmtContext context) =>
        new LengthNode
        {
            Value = context.lengthArgStr().Map(x => VisitQuotedString(x.quotedString())).StringValue()!,
            ErrorMessage = context.errorMessageStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            ErrorAppTag = context.errorAppTagStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Description = context.descriptionStmt().MapSingle(x => (StringNode)VisitDescriptionStmt(x))?.Value,
            Reference = context.referenceStmt().MapSingle(x => (StringNode)VisitReferenceStmt(x))?.Value
        };

    public override INode VisitPatternStmt(YangRfcParser.PatternStmtContext context) =>
        new PatternNode
        {
            Value = context.patternArgStr().Map(x => VisitQuotedString(x.quotedString())).StringValue()!,
            Modifier = context.modifierStmt().MapSingle(x => x.modifierArgStr())?.GetText() == "invert-match" 
                ? PatternModifier.InvertMatch 
                : PatternModifier.None,
            ErrorMessage = context.errorMessageStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            ErrorAppTag = context.errorAppTagStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Description = context.descriptionStmt().MapSingle(x => (StringNode)VisitDescriptionStmt(x))?.Value,
            Reference = context.referenceStmt().MapSingle(x => (StringNode)VisitReferenceStmt(x))?.Value
        };

    public override INode VisitEnumSpecification(YangRfcParser.EnumSpecificationContext context) =>
        new EnumSpecificationNode
        {
            Members = context.enumStmt().Select(x => (EnumSpecifiationMemberNode)VisitEnumStmt(x)).ToList()
        };

    public override INode VisitEnumStmt(YangRfcParser.EnumStmtContext context) =>
        new EnumSpecifiationMemberNode
        {
            Key = context.identifier().GetText(),
            Value = context.valueStmt()?.MapSingle(x => (int?)int.Parse(x.integerValueStr().GetText())),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
        };

    public override INode VisitIdentityrefSpecification(YangRfcParser.IdentityrefSpecificationContext context) =>
        new IdentityReferenceNode
        {
            References = context.baseStmt().Map(ParseBaseStmts) ?? new List<string>()
        };

    public override INode VisitBitsSpecification(YangRfcParser.BitsSpecificationContext context) =>
        new BitsSpecificationNode
        {
            Bits = context.bitStmt()
                .Select(x => (BitSpecificationNode)VisitBitStmt(x))
                .ToList()
        };

    public override INode VisitBitStmt(YangRfcParser.BitStmtContext context) =>
        new BitSpecificationNode
        {
            Position = context.positionStmt()
                .MapSingle(x => x.positionValueArgStr())
                .Map(x => (int?)int.Parse(x.GetText())),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt())
        };

    public override INode VisitBinarySpecification(YangRfcParser.BinarySpecificationContext context) =>
        new BinarySpecificationNode
        {
            Length = context.lengthStmt().Map(x => (LengthNode)VisitLengthStmt(x)),
        };

    public override INode VisitUnionSpecification(YangRfcParser.UnionSpecificationContext context) =>
        new UnionSpecificationNode
        {
            Types = context.typeStmt().Select(x => (TypeNode)VisitTypeStmt(x)).ToList()
        };

    public override INode VisitMustStmt(YangRfcParser.MustStmtContext context) =>
        new MustNode
        {
            Condition = context.Map(x => VisitQuotedString(x.quotedString())).StringValue()!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            ErrorAppTag = context.errorAppTagStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            ErrorMessage = context.errorMessageStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
        };

    public override INode VisitWhenStmt(YangRfcParser.WhenStmtContext context)
    {
        return new WhenNode
        {
            Condition = context.Map(x => VisitQuotedString(x.quotedString())).StringValue()!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue()
        };
    }

    public override INode VisitInputStmt(YangRfcParser.InputStmtContext context) =>
        new InputNode
        {
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList()
        };

    public override INode VisitOutputStmt(YangRfcParser.OutputStmtContext context) =>
        new OutputNode
        {
            Must = new MustSpecificationNode
            {
                Statements = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList()
            },
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList()
        };

    public override INode VisitIdentityStmt(YangRfcParser.IdentityStmtContext context) =>
        new IdentityNode
        {
            Identifier = context.identifier().GetText(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Description = context.descriptionStmt().MapSingle(x => (StringNode)VisitDescriptionStmt(x))?.Value,
            Reference = context.referenceStmt().MapSingle(x => (StringNode)VisitReferenceStmt(x))?.Value,
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Base = ParseBaseStmts(context.baseStmt()),
        };

    public override INode VisitUniqueStmt(YangRfcParser.UniqueStmtContext context) => context.uniqueArgStr().Map(x => VisitQuotedString(x.quotedString()))!;

    public override INode VisitQuotedString(YangRfcParser.QuotedStringContext context) => new StringNode(context.GetContentText());
    public override INode VisitDescriptionStmt(YangRfcParser.DescriptionStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;
    public override INode VisitReferenceStmt(YangRfcParser.ReferenceStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;
    
    private Status ParseStatus(YangRfcParser.StatusStmtContext context) => 
        context.statusArgStr().Map(x => Enum.Parse<Status>(x.GetText(), true));
    
    private OrderedBy ParseOrderedBy(YangRfcParser.OrderedByStmtContext context) => 
        context.orderedByArgStr().Map(x => Enum.Parse<OrderedBy>(x.GetText(), true));
    
    private List<string> ParseBaseStmts(IEnumerable<YangRfcParser.BaseStmtContext> context)
    {
        return context
            .Select(x => x.identifierRefArgStr())
            .Select(x => x.quotedString().Map(VisitQuotedString).StringValue() ?? x.GetText()).ToList();
    }
    
    private List<string> ParseIfFeatures(IEnumerable<YangRfcParser.IfFeatureStmtContext> context) =>
        context
            .Select(x => x.ifFeatureExprStr())
            .Select(x => VisitQuotedString(x.quotedString()))
            .Select(x => x.StringValue()!)
            .ToList();

    private List<INode> ParseBodyStmts(YangRfcParser.BodyStmtsContext context) =>
        context.children
            .Select(x => x switch
            {
                YangRfcParser.FeatureStmtContext featureContext => VisitFeatureStmt(featureContext),
                YangRfcParser.IdentityStmtContext identityStmtContext => VisitIdentityStmt(identityStmtContext),
                YangRfcParser.TypedefStmtContext typedefStmtContext => VisitTypedefStmt(typedefStmtContext),
                YangRfcParser.GroupingStmtContext groupingStmtContext => VisitGroupingStmt(groupingStmtContext),
                YangRfcParser.DataDefStmtContext dataDefStmtContext => VisitDataDefStmt(dataDefStmtContext),
                YangRfcParser.RpcStmtContext rpcStmtContext => VisitRpcStmt(rpcStmtContext),
                YangRfcParser.NotificationStmtContext notificationStmtContext => VisitNotificationStmt(notificationStmtContext),
                _ => null
            })
            .Where(x => x != null)
            .Select(x => x!)
            .ToList();
}