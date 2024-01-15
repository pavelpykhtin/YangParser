using Antlr4.Runtime;
using YangParser;

namespace TestProject1.Helpers;

public static class ParserHelpers
{
    public static YangRfcParser CreateParser(string filePath)
    {
        using var input = File.OpenText(filePath);

        var inputStream = new AntlrInputStream(input);
        var lexer = new YangRfcLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(lexer);
        var parser = new YangRfcParser(commonTokenStream);

        return parser;
    }
}