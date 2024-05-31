namespace Speck.TemporalBatching.Internal;

internal interface IPipeline<TRequest>
{
    void Push(Envelope<TRequest> envelope);
}
