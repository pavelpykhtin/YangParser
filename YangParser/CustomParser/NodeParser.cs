using System.Text;
using YangParser.Extensions;
using YangParser.Model;

namespace YangParser.CustomParser;

public class NodeParser
{
    private readonly Dictionary<string, INodeBodyParser> _bodyParsers;

    public NodeParser(Dictionary<string, INodeBodyParser> bodyParsers)
    {
        _bodyParsers = bodyParsers;
    }

    public INode Parse(StringReader input)
    {
        StringBuilder sb = new();
        var trimingLeading = true;

        while(true)
        {
            int readValue = input.Read();
            if(readValue == -1)
                break;
            
            char nextChar = (char)readValue;
            
            switch (trimingLeading)
            {
                case true:
                    if (nextChar.IsWhitespace())
                        continue;
                    trimingLeading = false;
                    sb.Append(nextChar);
                    break;
                case false:
                    if (nextChar.IsWhitespace())
                        break;
                    sb.Append(nextChar);
                    break;
            }

            if (nextChar.IsWhitespace())
                break;
        }
        
        return _bodyParsers[sb.ToString()].Parse(input);
    }
}