using FluentAssertions;
using YangParser;
using YangParser.CustomParser;
using YangParser.Model;

namespace TestProject1;

public class LeafNodeBodyParserTests
{
    private readonly LeafNodeBodyParser _parser;
    
    public LeafNodeBodyParserTests()
    {
        _parser = new LeafNodeBodyParser();
    }

    [Fact]
    public void ParseHandlesMultylineNodes()
    {
        var input = new StringReader(@"leafNode { 
            type string;
        }");
        
        var node = _parser.Parse(input);
        
        node.Should().BeOfType<LeafNode>();
        ((LeafNode)node).Identifier.Should().Be("leafNode");
        ((LeafNode)node).Type.Should().Be("string");
    }
    
    [Fact]
    public void ParseHandlesSingleLineNodes()
    {
        var input = new StringReader(@"leafNode { type string; }");
        
        var node = _parser.Parse(input);
        
        node.Should().BeOfType<LeafNode>();
        ((LeafNode)node).Identifier.Should().Be("leafNode");
        ((LeafNode)node).Type.Should().Be("string");
    }
    
    [Fact]
    public void ParseHandlesQuotedProperies()
    {
        var input = new StringReader(@"leafNode { 
            type string; 
            description 
                ""description value""; 
        }");
        
        var node = _parser.Parse(input);
        
        ((LeafNode)node).Description.Should().Be("description value");
    }
    
    [Theory]
    [InlineData(" leafNode { type string; }")]
    [InlineData("  leafNode { type string; }")]
    [InlineData("\nleafNode { type string; }")]
    [InlineData("\r\nleafNode { type string; }")]
    [InlineData("\tleafNode { type string; }")]
    public void ParseHandlesLeadingWhitespacesInName(string source)
    {
        var input = new StringReader(source);
        
        var node = _parser.Parse(input);
        
        node.Should().BeOfType<LeafNode>();
        ((LeafNode)node).Identifier.Should().Be("leafNode");
        ((LeafNode)node).Type.Should().Be("string");
    }
    
    [Theory]
    [InlineData("leafNode { type string; }")]
    [InlineData("leafNode   { type string; }")]
    [InlineData("leafNode\n{ type string; }")]
    [InlineData("leafNode\r\n{ type string; }")]
    [InlineData("leafNode\t{ type string; }")]
    public void ParseHandlesTrailingWhitespacesInName(string source)
    {
        var input = new StringReader(source);
        
        var node = _parser.Parse(input);
        
        node.Should().BeOfType<LeafNode>();
        ((LeafNode)node).Identifier.Should().Be("leafNode");
        ((LeafNode)node).Type.Should().Be("string");
    }
    
}