using System.Text;
using YangParser.Extensions;
using YangParser.Model;

namespace YangParser.CustomParser;

public class LeafNodeBodyParser : INodeBodyParser
{
    public INode Parse(TextReader input)
    {
        StringBuilder sb = new StringBuilder();
        var node = new LeafNode();
        var state = State.TrimmingNameLeadingWhitespaces;
        while (true)
        {
            int readValue = input.Read();
            if(readValue == -1)
                break;
            
            char nextChar = (char)readValue;

            switch (state)
            {
                case State.TrimmingNameLeadingWhitespaces:
                    if (nextChar.IsWhitespace())
                        continue;
                    state = State.ReadingName;
                    sb.Append(nextChar);
                    break;
                
                case State.ReadingName:
                    if (nextChar.IsWhitespace())
                    {
                        node.Identifier = sb.ToString();
                        sb.Clear();
                        state = State.TrimmingNameTrailingWhitespaces;
                    }
                    else
                    {
                        sb.Append(nextChar);
                    }
                    break;
                
                case State.TrimmingNameTrailingWhitespaces:
                    if (nextChar == '{')
                    {
                        state = State.ReadingProperties;
                    }
                    break;
                
                case State.ReadingProperties:
                    var properties = ReadProperties(input);
                    if (properties.Keys.Any())
                    {
                        //if(properties.ContainsKey("type")) node.Type = properties["type"];
                        if(properties.ContainsKey("description")) node.Description = properties["description"];
                        //if(properties.ContainsKey("range")) node.Range = properties["range"];
                    }
                    break;
            }
        }
        
        return node;
    }

    private Dictionary<string, string> ReadProperties(TextReader input)
    {
        var result = new Dictionary<string, string>();
        StringBuilder sb = new StringBuilder();
        var state = PropertyReaderState.TrimmingNameLeadingWhitespaces;
        var done = false;
        string name = string.Empty;

        while (!done)
        {
            int readValue = input.Read();
            if(readValue == -1)
                break;
            
            char nextChar = (char)readValue;

            switch (state)
            {
                case PropertyReaderState.TrimmingNameLeadingWhitespaces:
                    if (nextChar == '}')
                        done = true;
                    else if (!nextChar.IsWhitespace())
                    {
                        sb.Append(nextChar);
                        state = PropertyReaderState.ReadingName;
                    }
                    break;
                
                case PropertyReaderState.ReadingName:
                    if (nextChar.IsWhitespace())
                    {
                        name = sb.ToString();
                        sb.Clear();
                        state = PropertyReaderState.TrimmingNameTrailingWhitespaces;
                    }
                    else
                    {
                        sb.Append(nextChar);
                    }
                    break;
                
                case PropertyReaderState.TrimmingNameTrailingWhitespaces:
                    if (nextChar == '"')
                    {
                        state = PropertyReaderState.ReadingQuotedValue;
                    }
                    else if (!nextChar.IsWhitespace())
                    {
                        sb.Append(nextChar);
                        state = PropertyReaderState.ReadingLiteralValue;
                    }
                    break;
                
                case PropertyReaderState.ReadingQuotedValue:
                    if (nextChar == '"')
                    {
                        result.Add(name, sb.ToString());
                        sb.Clear();
                        state = PropertyReaderState.TrimmingValueTrailingWhitespaces;
                    }
                    else
                    {
                        sb.Append(nextChar);
                    }
                    break;
                
                case PropertyReaderState.ReadingLiteralValue:
                    if (nextChar.IsWhitespace())
                    {
                        result.Add(name, sb.ToString());
                        sb.Clear();
                        state = PropertyReaderState.TrimmingValueTrailingWhitespaces;
                    }
                    else if (nextChar == ';')
                    {
                        result.Add(name, sb.ToString());
                        sb.Clear();
                        state = PropertyReaderState.TrimmingNameLeadingWhitespaces;
                    }
                    else
                    {
                        sb.Append(nextChar);
                    }
                    break;
                
                case PropertyReaderState.TrimmingValueTrailingWhitespaces:
                    if (nextChar == ';')
                    {
                        state = PropertyReaderState.TrimmingNameLeadingWhitespaces;
                    }
                    break;
            }
        }

        return result;
    }

    private enum State
    {
        TrimmingNameLeadingWhitespaces,
        ReadingName,
        TrimmingNameTrailingWhitespaces,
        ReadingProperties
    }

    private enum PropertyReaderState
    {
        TrimmingNameLeadingWhitespaces,
        ReadingName,
        TrimmingNameTrailingWhitespaces,
        ReadingLiteralValue,
        ReadingQuotedValue,
        TrimmingValueTrailingWhitespaces
    }
}