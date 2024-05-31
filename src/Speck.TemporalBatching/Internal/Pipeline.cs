using System.Threading.Tasks.Dataflow;

namespace Speck.TemporalBatching.Internal;

internal class Pipeline<TRequest>(ITargetBlock<Envelope<TRequest>> block) : IPipeline<TRequest>
{
    public void Push(Envelope<TRequest> envelope) => block.Post(envelope);
}