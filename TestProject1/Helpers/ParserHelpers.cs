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
        parser.AddErrorListener(new ThrowErrorListener());
        
        return parser;
    }

    private class ThrowErrorListener : IAntlrErrorListener<IToken>
    {
        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new Exception($"{msg}\r\nat {line}:{charPositionInLine}");
        }
    }
}