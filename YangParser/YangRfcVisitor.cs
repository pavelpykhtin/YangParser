using Antlr4.Runtime.Tree;
using YangParser.Extensions;
using YangParser.GrammarModel;
using YangParser.Model;

namespace YangParser;

public class YangRfcVisitor : YangRfcParserBaseVisitor<INode>
{
    public override INode VisitModuleStmt(YangRfcParser.ModuleStmtContext context) =>
        new ModuleNode
        {
            Identifier = context.identifier().GetText(),
            Namespace = context.moduleHeaderStmts()
                .namespaceStmt()
                .Select(x => x.uriStr())
                .Select(x => VisitQuotedString(x.quotedString()).StringValue()!)
                .Single(),
            Prefix = context.moduleHeaderStmts()
                .prefixStmt()
                .Select(x => ParseOptionalQuotedString(x.prefixArgStr(), q => q.quotedString())!)
                .Single(),
            YangVersion = context.moduleHeaderStmts()
                .yangVersionStmt()
                .Select(x => ParseOptionalQuotedString(x.yangVersionArgStr(), q => q.quotedString())!)
                .SingleOrDefault(),
            Organization = context.metaStmts()
                .organizationStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue())!,
            Contact = context.metaStmts()
                .contactStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue())!,
            Description = context.metaStmts()
                .descriptionStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue())!,
            Reference = context.metaStmts()
                .referenceStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue())!,
            Revisions = context.revisionStmts()
                            .Map(x => x.revisionStmt())
                            .Map(x => x.Select(r => (RevisionNode)VisitRevisionStmt(r)).ToList())
                        ?? new List<RevisionNode>(),
            Imports = context.linkageStmts()
                .Map(x => x.importStmt())
                .Map(x => x.Select(i => (ImportNode)VisitImportStmt(i)).ToList()) ?? new List<ImportNode>(),
            Includes = context.linkageStmts()
                .Map(x => x.includeStmt())
                .Map(x => x.Select(i => (IncludeNode)VisitIncludeStmt(i)).ToList()) ?? new List<IncludeNode>(),
            Body = context.bodyStmts().Map(ParseBodyStmts) ?? new List<INode>(),
        };

    public override INode VisitSubmoduleStmt(YangRfcParser.SubmoduleStmtContext context) =>
        new SubmoduleNode
        {
            Identifier = context.identifier().GetText(),
            YangVersion = context.submoduleHeaderStmts()
                .yangVersionStmt()
                .Select(x => ParseOptionalQuotedString(x.yangVersionArgStr(), q => q.quotedString())!)
                .Single(),
            BelongsTo = context.submoduleHeaderStmts()
                .belongsToStmt()
                .Select(x => (BelongsToNode)VisitBelongsToStmt(x))
                .Single(),
            Organization = context.metaStmts()
                .organizationStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue()),
            Contact = context.metaStmts()
                .contactStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue()),
            Description = context.metaStmts()
                .descriptionStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue()),
            Reference = context.metaStmts()
                .referenceStmt()
                .MapSingle(x => VisitQuotedString(x.quotedString()).StringValue()),
            Revisions = context.revisionStmts()
                            .Map(x => x.revisionStmt())
                            .Map(x => x.Select(r => (RevisionNode)VisitRevisionStmt(r)).ToList())
                        ?? new List<RevisionNode>(),
            Imports = context.linkageStmts()
                .Map(x => x.importStmt())
                .Map(x => x.Select(i => (ImportNode)VisitImportStmt(i)).ToList()) ?? new List<ImportNode>(),
            Includes = context.linkageStmts()
                .Map(x => x.includeStmt())
                .Map(x => x.Select(i => (IncludeNode)VisitIncludeStmt(i)).ToList()) ?? new List<IncludeNode>(),
            Body = context.bodyStmts().Map(ParseBodyStmts) ?? new List<INode>(),
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
            Default = context.defaultStmt().MapSingle(ParseDefault),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(ParseConfig),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
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
            Default = context.defaultStmt().MapSingle(ParseDefault),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(ParseConfig),
            MinElements = context.minElementsStmt().MapSingle(x => ParseMinValue(x.minValueArgStr())),
            MaxElements = context.maxElementsStmt().MapSingle(x => ParseMaxValue(x.maxValueArgStr())),
            OrderedBy = context.orderedByStmt().MapSingle(ParseOrderedBy),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
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
            MinElements = context.minElementsStmt().MapSingle(x => ParseMinValue(x.minValueArgStr())),
            MaxElements = context.maxElementsStmt().MapSingle(x => ParseMaxValue(x.maxValueArgStr())),
            OrderedBy = context.orderedByStmt().MapSingle(ParseOrderedBy),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Keys = context.keyStmt().MapSingle(x => ParseKeysArgStr(x.keyArgStr()))!,
            Notifications = context.notificationStmt().Select(x => (NotificationNode)VisitNotificationStmt(x)).ToList(),
            Actions = context.actionStmt().Select(x => (ActionNode)VisitActionStmt(x)).ToList(),
            UniqueConstraints = context.uniqueStmt()
                .Select(x => ParseUnique(x)!)
                .ToList()
        };

    public override INode VisitContainerStmt(YangRfcParser.ContainerStmtContext context) =>
        new ContainerNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Config = context.configStmt().MapSingle(ParseConfig),
            Presence = context.presenceStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
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
            Default = context.defaultStmt().MapSingle(ParseDefault),
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

    public override INode VisitAnydataStmt(YangRfcParser.AnydataStmtContext context)
    {
        return new AnyDataNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Config = context.configStmt().MapSingle(ParseConfig),
            Mandatory = context.mandatoryStmt().MapSingle(x => bool.Parse(x.mandatoryArgStr().GetText())),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };
    }

    public override INode VisitAnyxmlStmt(YangRfcParser.AnyxmlStmtContext context)
    {
        return new AnyXmlNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Config = context.configStmt().MapSingle(ParseConfig),
            Mandatory = context.mandatoryStmt().MapSingle(x => bool.Parse(x.mandatoryArgStr().GetText())),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };
    }

    public override INode VisitChoiceStmt(YangRfcParser.ChoiceStmtContext context)
    {
        return new ChoiceNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            Config = context.configStmt().MapSingle(ParseConfig),
            Default = context.defaultStmt().MapSingle(ParseDefault),
            Mandatory = context.mandatoryStmt().MapSingle(x => bool.Parse(x.mandatoryArgStr().GetText())),
            ShortCases = context.shortCaseStmt().Select(VisitShortCaseStmt).ToList(),
            Cases = context.caseStmt().Select(stmtContext => (CaseNode)VisitCaseStmt(stmtContext)).ToList(),
        };
    }

    public override INode VisitNotificationStmt(YangRfcParser.NotificationStmtContext context) =>
        new NotificationNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList()
        };

    public override INode VisitDataDefStmt(YangRfcParser.DataDefStmtContext context) =>
        context.containerStmt().Map(VisitContainerStmt)
        ?? context.leafStmt().Map(VisitLeafStmt)
        ?? context.leafListStmt().Map(VisitLeafListStmt)
        ?? context.listStmt().Map(VisitListStmt)
        ?? context.choiceStmt().Map(VisitChoiceStmt)
        ?? context.anydataStmt().Map(VisitAnydataStmt)
        ?? context.anyxmlStmt().Map(VisitAnyxmlStmt)
        ?? context.usesStmt().Map(VisitUsesStmt)
        ?? throw new NotSupportedException("Unknown data-definition type");

    public override INode VisitTypeStmt(YangRfcParser.TypeStmtContext context)
    {
        var body = context.typeBodyStmts();
        var leafrefContext = body?.leafrefSpecification();

        return new TypeNode
        {
            Identifier = context.identifierRefArgStr().Map(VisitIdentifierRefArgStr).StringValue()!,
            NumericRestrictions = body?.numericalRestrictions().Map(x => (NumericRestrictionsNode)VisitNumericalRestrictions(x)),
            StringRestrictions = body?.stringRestrictions().Map(x => (StringRestrictionsNode)VisitStringRestrictions(x)),
            EnumSpecification = body?.enumSpecification().Map(x => (EnumSpecificationNode)VisitEnumSpecification(x)),
            IdentityReference = body?.identityrefSpecification().Map(x => (IdentityReferenceNode)VisitIdentityrefSpecification(x)),
            BitsSpecification = body?.bitsSpecification().Map(x => (BitsSpecificationNode)VisitBitsSpecification(x)),
            BinarySpecification = body?.binarySpecification().Map(x => (BinarySpecificationNode)VisitBinarySpecification(x)),
            UnionSpecification = body?.unionSpecification().Map(x => (UnionSpecificationNode)VisitUnionSpecification(x)),
            Path = leafrefContext?.pathStmt().MapSingle(x => ParseOptionalQuotedString(x.pathArgStr(), q => q.quotedString())),
            RequireInstance = leafrefContext?.requireInstanceStmt()
                .MapSingle(x => ParseOptionalQuotedString(x.requireInstanceArgStr(), q => q.quotedString()))
                .Map(x => (bool?)bool.Parse(x)),
        };
    }

    public override INode VisitCaseStmt(YangRfcParser.CaseStmtContext context) =>
        new CaseNode
        {
            Identifier = context.identifier().GetText(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
        };

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
            FractionDigits = context.fractionDigitsStmt()
                .MapSingle(x => ParseOptionalQuotedString(x.fractionDigitsArgStr(), q => q.quotedString()))
                .Map(x => (int?)int.Parse(x)),
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
            Modifier = context.modifierStmt()
                .MapSingle(x => x.modifierArgStr())
                .Map(x => ParseOptionalQuotedString(x, q => q.quotedString())) == "invert-match"
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
            Key = context.identifierOrQuotedString().Map(VisitIdentifierOrQuotedString).StringValue()!,
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
                .MapSingle(x => ParseOptionalQuotedString(x.positionValueArgStr(), q => q.quotedString()))
                .Map(x => (int?)int.Parse(x)),
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
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Groupings = context.groupingStmt().Select(x => (GroupingNode)VisitGroupingStmt(x)).ToList(),
            Typedefs = context.typedefStmt().Select(x => (TypedefNode)VisitTypedefStmt(x)).ToList()
        };

    public override INode VisitOutputStmt(YangRfcParser.OutputStmtContext context) =>
        new OutputNode
        {
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
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

    public override INode VisitExtensionStmt(YangRfcParser.ExtensionStmtContext context) =>
        new ExtensionNode
        {
            Identifier = context.identifier().GetText(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            Description = context.descriptionStmt().MapSingle(x => (StringNode)VisitDescriptionStmt(x))?.Value,
            Reference = context.referenceStmt().MapSingle(x => (StringNode)VisitReferenceStmt(x))?.Value,
            Argument = context.argumentStmt().MapSingle(x => (ArgumentNode)VisitArgumentStmt(x))
        };

    public override INode VisitArgumentStmt(YangRfcParser.ArgumentStmtContext context) =>
        new ArgumentNode
        {
            Identifier = context.identifier().GetText(),
            YinElement = context.yinElementStmt()
                .Map(x => x.yinElementArgStr())
                .Map(x => ParseOptionalQuotedString(x, q => q.quotedString()))
                .Map(x => (bool?)bool.Parse(x)),
        };

    public override INode VisitRevisionStmt(YangRfcParser.RevisionStmtContext context) =>
        new RevisionNode
        {
            Date = context.revisionDate()
                .Map(x => ParseOptionalQuotedString(x.dateArgStr(), q => q.quotedString()))
                .Map(x => DateOnly.ParseExact(x, "yyyy-MM-dd")),
            Description = context.descriptionStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Reference = context.referenceStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue()
        };

    public override INode VisitRefineStmt(YangRfcParser.RefineStmtContext context)
    {
        return new RefineNode
        {
            Argument = ParseOptionalQuotedString(context.refineArgStr(), q => q.quotedString())!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Presence = context.presenceStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue()!,
            Config = context.configStmt().MapSingle(ParseConfig),
            Mandatory = context.mandatoryStmt().MapSingle(x => x.mandatoryArgStr().GetText()).Map(bool.Parse),
            Default = context.defaultStmt()
                .Select(x => ParseDefault(x)!)
                .ToList(),
            MinElements = context.minElementsStmt().MapSingle(x => ParseMinValue(x.minValueArgStr())),
            MaxElements = context.maxElementsStmt().MapSingle(x => ParseMaxValue(x.maxValueArgStr())),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
        };
    }

    public override INode VisitAugmentStmt(YangRfcParser.AugmentStmtContext context) =>
        new AugmentNode
        {
            Argument = ParseOptionalQuotedString(context.augmentArgStr(), q => q.quotedString())!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            DataDefinitions = context.dataDefStmt().Select(VisitDataDefStmt).ToList(),
            Cases = context.caseStmt().Select(x => (CaseNode)VisitCaseStmt(x)).ToList(),
            Actions = context.actionStmt().Select(x => (ActionNode)VisitActionStmt(x)).ToList(),
            Notifications = context.notificationStmt().Select(x => (NotificationNode)VisitNotificationStmt(x)).ToList()
        };

    public override INode VisitUsesStmt(YangRfcParser.UsesStmtContext context) =>
        new UsesNode
        {
            Identifier = context.identifierRefArgStr().Map(VisitIdentifierRefArgStr).StringValue()!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(ParseStatus),
            IfFeatures = ParseIfFeatures(context.ifFeatureStmt()),
            When = context.whenStmt().MapSingle(x => (WhenNode)VisitWhenStmt(x)),
            Refine = context.refineStmt().Select(x => (RefineNode)VisitRefineStmt(x)).ToList(),
            Augment = context.augmentStmt().Select(x => (AugmentNode)VisitAugmentStmt(x)).ToList(),
        };

    public override INode VisitImportStmt(YangRfcParser.ImportStmtContext context) =>
        new ImportNode
        {
            Identifier = context.identifier().Map(x => x.GetText())!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Prefix = context.prefixStmt().MapSingle(x => x.prefixArgStr().GetText())!,
            RevisionDate = context.revisionDateStmt().MapSingle(x => DateOnly.ParseExact(x.revisionDate().GetText(), "yyyy-MM-dd")),
        };

    public override INode VisitIncludeStmt(YangRfcParser.IncludeStmtContext context) =>
        new IncludeNode
        {
            Identifier = context.identifier().Map(x => x.GetText())!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            RevisionDate = context.revisionDateStmt().MapSingle(x => DateOnly.ParseExact(x.revisionDate().GetText(), "yyyy-MM-dd")),
        };

    public override INode VisitBelongsToStmt(YangRfcParser.BelongsToStmtContext context) =>
        new BelongsToNode
        {
            Identifier = context.identifier().Map(x => x.GetText())!,
            Prefix = context.prefixStmt().Map(x => x.prefixArgStr().GetText())!
        };

    public override INode VisitDeviationStmt(YangRfcParser.DeviationStmtContext context) =>
        new DeviationNode
        {
            Argument = context.Map(x => VisitQuotedString(x.quotedString())).StringValue()!,
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Deviates = Enumerable.Empty<INode>()
                .Concat(context.deviateAddStmt().Map(x => x.Select(VisitDeviateAddStmt).ToList()) ?? new())
                .Concat(context.deviateDeleteStmt().Map(x => x.Select(VisitDeviateDeleteStmt).ToList()) ?? new())
                .Concat(context.deviateReplaceStmt().Map(x => x.Select(VisitDeviateReplaceStmt).ToList()) ?? new())
                .Concat(context.deviateNotSupportedStmt().MapSingle(_ => new[] { new DeviateNotSupportedNode() }) ?? Array.Empty<DeviateNotSupportedNode>())
                .ToList()
        };

    public override INode VisitDeviateAddStmt(YangRfcParser.DeviateAddStmtContext context) =>
        new DeviateAddNode
        {
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(ParseConfig),
            Mandatory = context.mandatoryStmt().MapSingle(x => x.mandatoryArgStr().GetText()).Map(bool.Parse),
            MinElements = context.minElementsStmt().MapSingle(x => ParseMinValue(x.minValueArgStr())),
            MaxElements = context.maxElementsStmt().MapSingle(x => ParseMaxValue(x.maxValueArgStr())),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            UniqueConstraints = context.uniqueStmt()
                .Select(x => ParseUnique(x)!)
                .ToList(),
            Default = context.defaultStmt()
                .Select(x => ParseDefault(x)!)
                .ToList(),
        };

    public override INode VisitDeviateReplaceStmt(YangRfcParser.DeviateReplaceStmtContext context) =>
        new DeviateReplaceNode
        {
            Type = context.typeStmt().MapSingle(x => (TypeNode)VisitTypeStmt(x)),
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Config = context.configStmt().MapSingle(ParseConfig),
            Mandatory = context.mandatoryStmt().MapSingle(x => x.mandatoryArgStr().GetText()).Map(bool.Parse),
            MinElements = context.minElementsStmt().MapSingle(x => ParseMinValue(x.minValueArgStr())),
            MaxElements = context.maxElementsStmt().MapSingle(x => ParseMaxValue(x.maxValueArgStr())),
            Default = context.defaultStmt()
                .Select(x => ParseDefault(x)!)
                .ToList(),
        };

    public override INode VisitDeviateDeleteStmt(YangRfcParser.DeviateDeleteStmtContext context) =>
        new DeviateDeleteNode
        {
            Units = context.unitsStmt().MapSingle(x => VisitQuotedString(x.quotedString())).StringValue(),
            Must = context.mustStmt().Select(x => (MustNode)VisitMustStmt(x)).ToList(),
            UniqueConstraints = context.uniqueStmt()
                .Select(x => ParseUnique(x)!)
                .ToList(),
            Default = context.defaultStmt()
                .Select(x => ParseDefault(x)!)
                .ToList(),
        };

    public override INode VisitQuotedString(YangRfcParser.QuotedStringContext context) => new StringNode(context.GetContentText());
    public override INode VisitDescriptionStmt(YangRfcParser.DescriptionStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;
    public override INode VisitReferenceStmt(YangRfcParser.ReferenceStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;

    private Status ParseStatus(YangRfcParser.StatusStmtContext context) =>
        ParseOptionalQuotedString(context.statusArgStr(), q => q.quotedString())
            .Map(x => Enum.Parse<Status>(x, true));

    private OrderedBy ParseOrderedBy(YangRfcParser.OrderedByStmtContext context) =>
        ParseOptionalQuotedString(context.orderedByArgStr(), q => q.quotedString())
            .Map(x => Enum.Parse<OrderedBy>(x, true));

    private List<string> ParseBaseStmts(IEnumerable<YangRfcParser.BaseStmtContext> context) =>
        context
            .Select(x => VisitIdentifierRefArgStr(x.identifierRefArgStr()).StringValue()!)
            .ToList();

    private bool? ParseConfig(YangRfcParser.ConfigStmtContext context) =>
        ParseOptionalQuotedString(context.configArgStr(), q => q.quotedString())
            .Map(x => (bool?)bool.Parse(x));

    public override INode VisitIdentifierOrQuotedString(YangRfcParser.IdentifierOrQuotedStringContext context) =>
        context.quotedString().Map(VisitQuotedString) ?? new StringNode(context.GetText());

    public override INode VisitIdentifierRefArgStr(YangRfcParser.IdentifierRefArgStrContext context) =>
        context.quotedString().Map(VisitQuotedString) ?? new StringNode(context.GetText());

    private List<string> ParseIfFeatures(IEnumerable<YangRfcParser.IfFeatureStmtContext> context) =>
        context
            .Select(x => x.ifFeatureExprStr())
            .Select(x => VisitQuotedString(x.quotedString()))
            .Select(x => x.StringValue()!)
            .ToList();

    private List<string> ParseKeysArgStr(YangRfcParser.KeyArgStrContext context) =>
        ParseOptionalQuotedString(context, q => q.quotedString())
            .Map(x => x.Split(
                    new[] { ' ', '\t', '\r', '\n' },
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToList())
        ?? new();

    private List<INode> ParseBodyStmts(YangRfcParser.BodyStmtsContext context) =>
        (context.children ?? new List<IParseTree>())
        .Select(x => x switch
        {
            YangRfcParser.ExtensionStmtContext extensionStmtContext => VisitExtensionStmt(extensionStmtContext),
            YangRfcParser.FeatureStmtContext featureContext => VisitFeatureStmt(featureContext),
            YangRfcParser.IdentityStmtContext identityStmtContext => VisitIdentityStmt(identityStmtContext),
            YangRfcParser.TypedefStmtContext typedefStmtContext => VisitTypedefStmt(typedefStmtContext),
            YangRfcParser.GroupingStmtContext groupingStmtContext => VisitGroupingStmt(groupingStmtContext),
            YangRfcParser.DataDefStmtContext dataDefStmtContext => VisitDataDefStmt(dataDefStmtContext),
            YangRfcParser.AugmentStmtContext augmentStmtContext => VisitAugmentStmt(augmentStmtContext),
            YangRfcParser.RpcStmtContext rpcStmtContext => VisitRpcStmt(rpcStmtContext),
            YangRfcParser.NotificationStmtContext notificationStmtContext => VisitNotificationStmt(notificationStmtContext),
            YangRfcParser.DeviationStmtContext deviationStmtContext => VisitDeviationStmt(deviationStmtContext),
            _ => null,
        })
        .Where(x => x != null)
        .Select(x => x!)
        .ToList();

    public override INode VisitShortCaseStmt(YangRfcParser.ShortCaseStmtContext context) =>
        context.choiceStmt().Map(VisitChoiceStmt)
        ?? context.containerStmt().Map(VisitContainerStmt)
        ?? context.leafStmt().Map(VisitLeafStmt)
        ?? context.leafListStmt().Map(VisitLeafListStmt)
        ?? context.listStmt().Map(VisitListStmt)
        ?? context.anydataStmt().Map(VisitAnydataStmt)
        ?? context.anyxmlStmt().Map(VisitAnyxmlStmt)
        ?? throw new NotImplementedException();

    private string? ParseDefault(YangRfcParser.DefaultStmtContext context) =>
        ParseOptionalQuotedString(context.defaultArgStr(), q => q.quotedString());

    private int? ParseMinValue(YangRfcParser.MinValueArgStrContext context) =>
        ParseOptionalQuotedString(context, q => q.quotedString())
            .Map(x => (int?)int.Parse(x));

    private int? ParseMaxValue(YangRfcParser.MaxValueArgStrContext context) =>
        ParseOptionalQuotedString(context, q => q.quotedString())
            .Map(x => (int?)int.Parse(x));
    
    private string? ParseUnique(YangRfcParser.UniqueStmtContext context) =>
        ParseOptionalQuotedString(context.uniqueArgStr(), q => q.quotedString());

    private string? ParseOptionalQuotedString<TContext>(TContext context, Func<TContext, YangRfcParser.QuotedStringContext> quotedString)
        where TContext : IParseTree =>
        quotedString(context)
            .Map(VisitQuotedString)
            .StringValue()
        ?? context.GetText();
}