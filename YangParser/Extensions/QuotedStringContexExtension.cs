using YangParser.Model;

namespace YangParser.Extensions;

public static class QuotedStringContexExtension
{
    public static string? GetContentText(this YangRfcParser.QuotedStringContext context) => 
        context.dquoteContent()?.GetText().Trim('"') 
        ?? context.squoteContent()?.GetText().Trim('\'');

    public static string? StringValue(this INode? node) => ((StringNode?)node)?.Value;
}