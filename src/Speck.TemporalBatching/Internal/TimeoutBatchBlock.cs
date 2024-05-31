using System.Threading.Tasks.Dataflow;

namespace Speck.TemporalBatching.Internal;

internal static class TimeoutBatchBlock
{
    public static IPropagatorBlock<Envelope<TSource>, Envelope<TSource>[]> Create<TSource>(int batchSize, TimeSpan timeout)
    {
        var batchBlock = new BatchBlock<Envelope<TSource>>(batchSize);

        var timer = new Timer(_ => batchBlock.TriggerBatch());

        var actionBlock = new ActionBlock<Envelope<TSource>>(item =>
        {
            batchBlock.Post(item);

            timer.Change(timeout, Timeout.InfiniteTimeSpan);
        });

        return DataflowBlock.Encapsulate(actionBlock, batchBlock);
    }
}
