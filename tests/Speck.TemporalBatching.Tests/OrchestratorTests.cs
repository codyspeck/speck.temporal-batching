using Speck.TemporalBatching.Internal;

namespace Speck.TemporalBatching.Tests;

public class OrchestratorTests
{
    [Fact]
    public async Task Disposes_task_completion_source()
    {
        var orchestrator = new Orchestrator();

        var guid = Guid.NewGuid();
        
        var taskCompletionSource = orchestrator.Add(guid);

        orchestrator.Keys.Should().ContainSingle();
        
        orchestrator.Complete(guid);

        await taskCompletionSource.Task;

        orchestrator.Keys.Should().BeEmpty();
    }
}
