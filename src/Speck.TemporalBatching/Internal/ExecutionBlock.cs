using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.DependencyInjection;

namespace Speck.TemporalBatching.Internal;

internal static class ExecutionBlock
{
    public static TransformBlock<Envelope<TRequest>[], Envelope<TRequest>[]> Create<TRequest>(
        IServiceProvider services,
        int maxDegreeOfParallelism)
    {
        return new TransformBlock<Envelope<TRequest>[], Envelope<TRequest>[]>(async envelopes =>
        {
            await using var scope = services.CreateAsyncScope();
            
            var handler = scope.ServiceProvider.GetRequiredService<IBatchHandler<TRequest>>();

            var requests = envelopes
                .Select(envelope => envelope.Request)
                .ToList();

            await handler.HandleAsync(requests);

            return envelopes;
        },
            new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            });
    }    
}
