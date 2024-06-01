using Microsoft.Extensions.DependencyInjection;

namespace Speck.TemporalBatching.Tests;

public class IntegrationTests
{
    private const int BatchSize = 10;
    
    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(100);
    
    [Fact]
    public async Task Flushes_and_completes_partially_full_batch()
    {
        await ExecuteInScopeAsync(async (processor, collection) =>
        {
            var request = new TestRequest();

            await processor.ProcessAsync(request);

            collection.HandledRequestBatches
                .Should().ContainSingle()
                .Which.Should().ContainSingle(r => r == request);
        });
    }

    [Fact]
    public async Task Batches_requests()
    {
        await ExecuteInScopeAsync(async (processor, collection) =>
        {
            var requests = Enumerable.Range(0, BatchSize * 10)
                .Select(_ => new TestRequest())
                .ToList();

            foreach (var request in requests)
            {
                _ = processor.ProcessAsync(request);
            }

            await Helpers.PollUntilAsync(() => requests
                .All(request => collection.HandledRequestBatches
                    .Any(batch => batch.Contains(request))));

            collection.HandledRequestBatches.Should().HaveCount(10);
        });
    }
    
    private static async Task ExecuteInScopeAsync(Func<IProcessor<TestRequest>, TestRequestCollection, Task> func)
    {
        await using var services = new ServiceCollection()
            .AddTemporalBatchProcessor<TestBatchHandler, TestRequest>(processor => processor
                .WithBatchSize(BatchSize)
                .WithTimeout(Timeout))
            .AddSingleton<TestRequestCollection>()
            .BuildServiceProvider();
        
        await using var scope = services.CreateAsyncScope();

        var processor = scope.ServiceProvider.GetRequiredService<IProcessor<TestRequest>>();
        var collection = scope.ServiceProvider.GetRequiredService<TestRequestCollection>();

        await func(processor, collection);
    }
}
