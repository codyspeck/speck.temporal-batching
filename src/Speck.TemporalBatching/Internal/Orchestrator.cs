using System.Collections.Concurrent;
using System.Diagnostics;

namespace Speck.TemporalBatching.Internal;

internal class Orchestrator : IOrchestrator
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource> _completions = new();

    internal ICollection<Guid> Keys => _completions.Keys; // For testing purposes.
    
    public TaskCompletionSource Add(Guid requestId)
    {
        var completion = new TaskCompletionSource();

        if (!_completions.TryAdd(requestId, completion))
            throw new UnreachableException($"Failed to add {nameof(TaskCompletionSource)} to {nameof(Orchestrator)}.");
        
        return completion;
    }

    public void Complete(Guid guid)
    {
        if (!_completions.TryRemove(guid, out var completion))
            throw new UnreachableException($"Attempting to complete missing {nameof(TaskCompletionSource)}.");

        completion.TrySetResult();
    }
}
