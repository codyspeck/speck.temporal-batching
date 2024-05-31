namespace Speck.TemporalBatching.Internal;

internal interface IOrchestrator
{
    public TaskCompletionSource Add(Guid requestId);

    public void Complete(Guid requestId);
}
