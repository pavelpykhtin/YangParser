using YangParser.Model;

namespace YangParser.CustomParser;

public interface INodeBodyParser
{
    INode Parse(TextReader reader);
}