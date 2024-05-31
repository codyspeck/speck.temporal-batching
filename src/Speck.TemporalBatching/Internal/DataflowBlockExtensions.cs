using System.Threading.Tasks.Dataflow;

namespace Speck.TemporalBatching.Internal;

internal static class DataflowBlockExtensions
{
    public static TTarget LinkToAndReturn<TSource, TTarget>(this ISourceBlock<TSource> source, TTarget target)
        where TTarget : ITargetBlock<TSource>
    {
        source.LinkTo(target, new DataflowLinkOptions { PropagateCompletion = true });
        return target;
    }    
}
