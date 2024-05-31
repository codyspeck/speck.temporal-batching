namespace Speck.TemporalBatching.Internal;

internal class Processor<TRequest>(IOrchestrator orchestrator, IPipeline<TRequest> pipeline) : IProcessor<TRequest>
{
    public async Task ProcessAsync(TRequest request)
    {
        var envelope = new Envelope<TRequest>(Guid.NewGuid(), request);
        
        var completionSource = orchestrator.Add(envelope.CorrelationId);
        
        pipeline.Push(envelope);

        await completionSource.Task;
    }
}
