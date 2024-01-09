namespace YangParser.Extensions;

public static class NullExtensions
{
    public static TResult? Map<TSource, TResult>(this TSource? source, Func<TSource, TResult?> func) => 
        source == null ? default : func(source);
    
    public static TResult? MapSingle<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func) =>
        source.SingleOrDefault().Map(func);
}