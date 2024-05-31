using System.Collections.Concurrent;
using System.Diagnostics;

namespace Speck.TemporalBatching.Internal;

internal class Orchestrator : IOrchestrator
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource> _completions = new();

    public TaskCompletionSource Add(Guid requestId)
    {
        var completion = new TaskCompletionSource();

        if (!_completions.TryAdd(requestId, completion))
            throw new UnreachableException("Failed to add TaskCompletionSource to TemporalRequestOrchestrator.");

        completion.Task.ContinueWith(_ =>
        {
            _completions.Remove(requestId, out var _);
        });

        return completion;
    }

    public void Complete(Guid guid)
    {
        if (!_completions.TryGetValue(guid, out var completion))
            throw new UnreachableException("Attempting to complete unknown TaskCompletionSource.");

        completion.TrySetResult();
    }
}
