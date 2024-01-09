using YangParser.Extensions;
using YangParser.Model;

namespace YangParser;

public class YangRfcVisitor: YangRfcBaseVisitor<INode>
{
    public override INode VisitModuleStmt(YangRfcParser.ModuleStmtContext context)
    {
        var moduleNode = new ModuleNode
        {
            Identifier = context.identifierArgStr().GetText()
        };
        
        base.VisitModuleStmt(context);
        
        return moduleNode;
    }

    public override INode VisitSubmoduleStmt(YangRfcParser.SubmoduleStmtContext context)
    {
        var submoduleNode = new SubmoduleNode
        {
            Identifier = context.identifierArgStr().GetText()
        };

        base.VisitSubmoduleStmt(context);
        
        return submoduleNode;
    }

    public override INode VisitFeatureStmt(YangRfcParser.FeatureStmtContext context)
    {
        var featureNode = new FeatureNode
        {
            Identifier = context.identifierArgStr().GetText(),
            Description = context.descriptionStmt().SingleOrDefault()
                ?.quotedString().GetContentText(),
            Reference = context.referenceStmt().SingleOrDefault()
                ?.quotedString().GetContentText(),
            Status = context.statusStmt().MapSingle(x => x.statusArgStr().GetText()).Map(x => Enum.Parse<Status>(x, true))
        };
        
        return featureNode;
    }

    public override INode VisitLeafStmt(YangRfcParser.LeafStmtContext context)
    {
        var leafNode = new LeafNode
        {
            Identifier = context.identifierArgStr().GetText(),
            Type = (TypeNode)VisitTypeStmt(context.typeStmt().Single()),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Mandatory = context.mandatoryStmt()
                .Select(x => bool.Parse(x.mandatoryArgStr().GetText()))
                .SingleOrDefault(),
            Status = context.statusStmt().MapSingle(x => x.statusArgStr().GetText()).Map(x => Enum.Parse<Status>(x, true))
        };

        return leafNode;
    }

    public override INode VisitTypeStmt(YangRfcParser.TypeStmtContext context)
    {
        var typeNode = new TypeNode
        {
            Identifier = context.identifierRefArgStr().GetText(),
        };
        var body = context.typeBodyStmts();
        if (body != null)
        {
            typeNode.NumericRestrictions = body.numericalRestrictions().Map(x => (NumericRestrictionsNode)VisitNumericalRestrictions(x));
            typeNode.StringRestrictions = body.stringRestrictions().Map(x => (StringRestrictionsNode)VisitStringRestrictions(x));
            typeNode.EnumSpecification = body.enumSpecification().Map(x => (EnumSpecificationNode)VisitEnumSpecification(x));
            typeNode.IdentityReference = body.identityrefSpecification().Map(x => (IdentityReferenceNode)VisitIdentityrefSpecification(x));
            typeNode.BitsSpecification = body.bitsSpecification().Map(x => (BitsSpecificationNode)VisitBitsSpecification(x));
            typeNode.BinarySpecification = body.binarySpecification().Map(x => (BinarySpecificationNode)VisitBinarySpecification(x));
            typeNode.UnionSpecification = body.unionSpecification().Map(x => (UnionSpecificationNode)VisitUnionSpecification(x));
            
            var leafrefContext = body.leafrefSpecification();
            if (leafrefContext != null)
            {
                typeNode.Path = leafrefContext.pathStmt().MapSingle(x => x.pathArgStr()).Map(x => VisitQuotedString(x.quotedString())).StringValue();
                typeNode.RequireInstance = leafrefContext.requireInstanceStmt().MapSingle(x => (bool?)bool.Parse(x.requireInstanceArgStr().GetText()));
            }
        }
        
        return typeNode;
    }

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

    public override INode VisitEnumStmt(YangRfcParser.EnumStmtContext context)
    {
        return new EnumSpecifiationMemberNode
        {
            Key = context.identifier().GetText(),
            Value = context.valueStmt()?.MapSingle(x => (int?)int.Parse(x.integerValueStr().GetText())),
            IfFeatures = context.ifFeatureStmt()
                .Select(x => x.ifFeatureExprStr())
                .Select(x => VisitQuotedString(x.quotedString()))
                .Select(x => x.StringValue()!)
                .ToList(),
            Description = context.descriptionStmt().MapSingle(VisitDescriptionStmt).StringValue(),
            Reference = context.referenceStmt().MapSingle(VisitReferenceStmt).StringValue(),
            Status = context.statusStmt().MapSingle(x => x.statusArgStr().GetText()).Map(x => Enum.Parse<Status>(x, true))
        };
    }

    public override INode VisitIdentityrefSpecification(YangRfcParser.IdentityrefSpecificationContext context) =>
        new IdentityReferenceNode
        {
            References = context.baseStmt()
                .Select(x => x.identifierRefArgStr())
                .Select(x => x.quotedString().Map(VisitQuotedString).StringValue() ?? x.GetText()).ToList()
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
            Status = context.statusStmt().MapSingle(x => x.statusArgStr().GetText()).Map(x => Enum.Parse<Status>(x, true)),
            IfFeatures = context.ifFeatureStmt()
                .Select(x => x.ifFeatureExprStr())
                .Select(x => VisitQuotedString(x.quotedString()))
                .Select(x => x.StringValue()!)
                .ToList()
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

    public override INode VisitQuotedString(YangRfcParser.QuotedStringContext context) => new StringNode(context.GetContentText());
    public override INode VisitDescriptionStmt(YangRfcParser.DescriptionStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;
    public override INode VisitReferenceStmt(YangRfcParser.ReferenceStmtContext context) => context.Map(x => VisitQuotedString(x.quotedString()))!;
}