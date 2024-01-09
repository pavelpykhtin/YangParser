using FluentAssertions;
using Moq;
using YangParser;
using YangParser.CustomParser;
using YangParser.Model;

namespace TestProject1;

public class NodeParserTests
{
    private readonly NodeParser _parser;
    private readonly Mock<INodeBodyParser> _bodyParserA;
    private readonly Mock<INodeBodyParser> _bodyParserB;

    public NodeParserTests()
    {
        _bodyParserA = new Mock<INodeBodyParser>();
        _bodyParserB = new Mock<INodeBodyParser>();
        _parser = new NodeParser(
                new Dictionary<string, INodeBodyParser> { 
                    {"nodeA", _bodyParserA.Object },
                    {"nodeB", _bodyParserB.Object }
                }
            );
    }
    
    [Fact]
    public void ParseCallsParserByNodeType()
    {
        var inputA = new StringReader(@"
            nodeA someA
                ""B"";");
        var inputB = new StringReader(@"
            nodeB someB
                ""B"";");
        
        _parser.Parse(inputA);
        _parser.Parse(inputB);
        
        _bodyParserA.Verify(x => x.Parse(inputA), Times.Once);
        _bodyParserB.Verify(x => x.Parse(inputB), Times.Once);
    }

    [Fact]
    public void ParseReturnsNodeBuiltByBodyParser()
    {
        var input = new StringReader(@"
            nodeA someA
                ""a"";");
        var dummyNode = new Mock<INode>().Object;
        
        _bodyParserA.Setup(x => x.Parse(input)).Returns(dummyNode);
        
        var node = _parser.Parse(input);
        
        node.Should().Be(dummyNode);
    }
    
    [Theory]
    [InlineData(@" nodeA someA ""a"";")]
    [InlineData(@"     nodeA someA ""a"";")]
    [InlineData(@"  nodeA someA ""a"";")]
    [InlineData("\nnodeA someA \"a\";")]
    [InlineData("\r\nnodeA someA \"a\";")]
    public void ParseHandlesLeadingWhitespaces(string source)
    {
        var input = new StringReader(source);
        var dummyNode = new Mock<INode>().Object;
        
        _bodyParserA.Setup(x => x.Parse(input)).Returns(dummyNode);
        
        var node = _parser.Parse(input);
        
        node.Should().Be(dummyNode);
    }
}