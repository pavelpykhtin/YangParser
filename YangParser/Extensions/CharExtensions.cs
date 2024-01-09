namespace YangParser.Extensions;

public static class CharExtensions
{
    public static bool IsWhitespace(this char c)
    {
        return c == ' ' || c == '\t' || c == '\n' || c == '\r';
    }
}