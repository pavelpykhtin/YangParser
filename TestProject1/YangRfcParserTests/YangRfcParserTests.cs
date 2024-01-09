using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using YangParser;
using YangParser.Model;

namespace TestProject1.YangRfcParserTests;

public class YangRfcParserTests
{
    private readonly YangRfcVisitor _visitor;

    public YangRfcParserTests()
    {
        _visitor = new YangRfcVisitor();
    }

    [Theory]
    [InlineData("YangRfcParserTests/sample1.yang", nameof(YangRfcParser.moduleStmt))]
    [InlineData("YangRfcParserTests/feature.yang", nameof(YangRfcParser.featureStmt))]
    public void Simple(string fileName, string contextAccessor)
    {
        YangRfcParser parser = CreateParser(fileName);

        var context = (IParseTree)typeof(YangRfcParser).GetMethod(contextAccessor)?.Invoke(parser, null)!;

        _visitor.Visit(context);
    }

    [Fact]
    public void HandlesFeature()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/feature.yang");

        var context = parser.featureStmt();

        var featureNode = (FeatureNode)_visitor.Visit(context);

        featureNode.Description.Should().Be("A server implements this feature if it can act as an SNMP proxy.");
        featureNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP)\r\n       Applications");
        featureNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesLeaf()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/leaf.yang");

        var context = parser.leafStmt();

        var leafNode = (LeafNode)_visitor.Visit(context);

        leafNode.Identifier.Should().Be("context-engine-id");
        leafNode.Type.Identifier.Should().Be("snmp:engine-id");
        leafNode.Mandatory.Should().BeTrue();
        leafNode.Reference.Should().Be("RFC 3413: Simple Network Management Protocol (SNMP).\r\n             Applications.\r\n             SNMP-PROXY-MIB.snmpProxyContextEngineID");
        leafNode.Status.Should().Be(Status.Current);
    }

    [Fact]
    public void HandlesTypeWithNumericalRestrictions()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-numerical.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("uint32");
        typeNode.NumericRestrictions!.Range.Should().Be("0..4294967295");
        typeNode.NumericRestrictions!.Description.Should().Be("Dummy description");
        typeNode.NumericRestrictions!.Reference.Should().Be("Dummy reference");
        typeNode.NumericRestrictions!.ErrorMessage.Should().Be("Dummy error message");
        typeNode.NumericRestrictions!.ErrorAppTag.Should().Be("Dummy error app tag");
    }

    [Fact]
    public void HandlesTypeWithDecimalRestrictions()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-decimal.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("decimal64");
        typeNode.NumericRestrictions!.FractionDigits.Should().Be(2);
        typeNode.NumericRestrictions!.Range.Should().Be("1 .. 3.14 | 10 | 20..max");
        typeNode.NumericRestrictions!.Description.Should().Be("Dummy description");
        typeNode.NumericRestrictions!.Reference.Should().Be("Dummy reference");
        typeNode.NumericRestrictions!.ErrorMessage.Should().Be("Dummy error message");
        typeNode.NumericRestrictions!.ErrorAppTag.Should().Be("Dummy error app tag");
    }

    [Fact]
    public void HandlesTypeWithStringRestrictions()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-string.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("string");
        typeNode.StringRestrictions!.Length!.Value.Should().Be("1..max");
        typeNode.StringRestrictions!.Length.Description.Should().Be("Dummy description");
        typeNode.StringRestrictions!.Length.Reference.Should().Be("Dummy reference");
        typeNode.StringRestrictions!.Length.ErrorMessage.Should().Be("Dummy error message");
        typeNode.StringRestrictions!.Length.ErrorAppTag.Should().Be("Dummy error app tag");
        
        typeNode.StringRestrictions!.Patterns.Should().HaveCount(2);
        typeNode.StringRestrictions!.Patterns[0].Value.Should().Be(@"[a-zA-Z_][a-zA-Z0-9\-_.]*");
        typeNode.StringRestrictions!.Patterns[0].InvertMatch.Should().BeFalse();
        typeNode.StringRestrictions!.Patterns[0].Modifier.Should().Be(PatternModifier.None);
        typeNode.StringRestrictions!.Patterns[0].Description.Should().BeNull();
        typeNode.StringRestrictions!.Patterns[0].Reference.Should().BeNull();
        typeNode.StringRestrictions!.Patterns[0].ErrorMessage.Should().BeNull();
        typeNode.StringRestrictions!.Patterns[0].ErrorAppTag.Should().BeNull();
        
        typeNode.StringRestrictions!.Patterns.Should().HaveCount(2);
        typeNode.StringRestrictions!.Patterns[1].Value.Should().Be(@"[xX][mM][lL].*");
        typeNode.StringRestrictions!.Patterns[1].InvertMatch.Should().BeTrue();
        typeNode.StringRestrictions!.Patterns[1].Modifier.Should().Be(PatternModifier.InvertMatch);
        typeNode.StringRestrictions!.Patterns[1].Description.Should().Be("Dummy description");
        typeNode.StringRestrictions!.Patterns[1].Reference.Should().Be("Dummy reference");
        typeNode.StringRestrictions!.Patterns[1].ErrorMessage.Should().Be("Dummy error message");
        typeNode.StringRestrictions!.Patterns[1].ErrorAppTag.Should().Be("Dummy error app tag");
    }

    [Fact]
    public void HandlesTypeWithEnumSpecification()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-enum.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("enumeration");
        typeNode.EnumSpecification!.Members.Should().HaveCount(4);
        
        typeNode.EnumSpecification!.Members[0].Key.Should().Be("zero");
        typeNode.EnumSpecification!.Members[0].Value.Should().NotHaveValue();
        typeNode.EnumSpecification!.Members[0].Status.Should().Be(Status.Current);
        
        typeNode.EnumSpecification!.Members[1].Key.Should().Be("one");
        typeNode.EnumSpecification!.Members[1].Value.Should().NotHaveValue();
        
        typeNode.EnumSpecification!.Members[2].Key.Should().Be("seven");
        typeNode.EnumSpecification!.Members[2].Value.Should().Be(7);
        typeNode.EnumSpecification!.Members[2].IfFeatures.Should().HaveCount(1).And.Contain("ssh");
        typeNode.EnumSpecification!.Members[2].Description.Should().Be("Dummy description");
        typeNode.EnumSpecification!.Members[2].Reference.Should().Be("Dummy reference");
        
        typeNode.EnumSpecification!.Members[3].Key.Should().Be("eight");
        typeNode.EnumSpecification!.Members[3].Value.Should().NotHaveValue();
    }

    [Fact]
    public void HandlesTypeWithLeafRef()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-leafref.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("leafref");
        typeNode.Path.Should().Be("/interface[name = current()/../if-name]/admin-status");
        typeNode.RequireInstance.Should().Be(false);
    }

    [Fact]
    public void HandlesTypeWithIdentityRef()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-identityref.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("identityref");
        typeNode.IdentityReference!.References.Should().Contain(new[]{"crypto:crypto-alg", "ssh"} );
    }

    [Fact]
    public void HandlesTypeWithBitsSpecification()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-bits.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("bits");
        
        typeNode.BitsSpecification!.Bits[0].Position.Should().Be(0);
        
        typeNode.BitsSpecification!.Bits[1].Position.Should().Be(1);
        typeNode.BitsSpecification!.Bits[1].Status.Should().Be(Status.Current);
        typeNode.BitsSpecification!.Bits[1].Description.Should().Be("Dummy description");
        typeNode.BitsSpecification!.Bits[1].Reference.Should().Be("Dummy reference");
        typeNode.BitsSpecification!.Bits[1].IfFeatures.Should().Contain("ssh", "ips");
        
        typeNode.BitsSpecification!.Bits[2].Position.Should().Be(2);
    }

    [Fact]
    public void HandlesTypeWithBinarySpecification()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-binary.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("binary");
        typeNode.StringRestrictions!.Length!.Value.Should().Be("1..max");
        typeNode.StringRestrictions!.Length.Description.Should().Be("Dummy description");
        typeNode.StringRestrictions!.Length.Reference.Should().Be("Dummy reference");
        typeNode.StringRestrictions!.Length.ErrorMessage.Should().Be("Dummy error message");
        typeNode.StringRestrictions!.Length.ErrorAppTag.Should().Be("Dummy error app tag");
    }

    [Fact]
    public void HandlesTypeWithUnionSpecification()
    {
        YangRfcParser parser = CreateParser("YangRfcParserTests/type-union.yang");

        var context = parser.typeStmt();

        var typeNode = (TypeNode)_visitor.Visit(context);

        typeNode.Identifier.Should().Be("union");
        typeNode.UnionSpecification!.Types.Should().HaveCount(2);
        typeNode.UnionSpecification!.Types[0].Identifier.Should().Be("int32");
        typeNode.UnionSpecification!.Types[1].Identifier.Should().Be("enumeration");
    }

    private YangRfcParser CreateParser(string filePath)
    {
        using var input = File.OpenText(filePath);

        AntlrInputStream inputStream = new AntlrInputStream(input);
        YangRfcLexer lexer = new YangRfcLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YangRfcParser parser = new YangRfcParser(commonTokenStream);

        return parser;
    }
}