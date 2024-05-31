using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.DependencyInjection;

namespace Speck.TemporalBatching.Internal;

internal static class CompletionBlock
{
    public static ActionBlock<Envelope<TRequest>[]> Create<TRequest>(IServiceProvider services)
    {
        var orchestrator = services.GetRequiredService<IOrchestrator>();
        
        return new ActionBlock<Envelope<TRequest>[]>(envelopes =>
        {
            foreach (var envelope in envelopes)
            {
                orchestrator.Complete(envelope.CorrelationId);
            }
        });
    }    
}
