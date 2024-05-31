namespace Speck.TemporalBatching.Internal;

internal static class PipelineBuilder
{
    public static IPipeline<TRequest> Build<TRequest>(IServiceProvider services, PipelineConfiguration configuration)
    {
        var block = TimeoutBatchBlock.Create<TRequest>(configuration.BatchSize, configuration.Timeout);
            
        block
            .LinkToAndReturn(ExecutionBlock.Create<TRequest>(services, configuration.MaxDegreeOfParallelism))
            .LinkToAndReturn(CompletionBlock.Create<TRequest>(services));

        return new Pipeline<TRequest>(block);
    }
}
