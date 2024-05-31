using Microsoft.Extensions.DependencyInjection;

namespace Speck.TemporalBatching.Tests;

public class Tests
{
    private readonly IServiceProvider _services;
    
    public Tests()
    {
        _services = new ServiceCollection()
            .AddTemporalBatchProcessor<TestBatchHandler, TestRequest>(processor => processor
                .WithBatchSize(10)
                .WithTimeout(TimeSpan.FromMilliseconds(200)))
            .AddSingleton<TestRequestCollection>()
            .BuildServiceProvider();
    }
    
    [Fact]
    public async Task Handles_single_request_and_blocks_until_completion()
    {
        await ExecuteInScopeAsync(async (processor, collection) =>
        {
            var request = new TestRequest();

            await processor.ProcessAsync(request);

            collection.IsHandled(request).Should().BeTrue();
        });
    }

    [Fact]
    public async Task Handles_many_requests()
    {
        await ExecuteInScopeAsync(async (processor, collection) =>
        {
            var requests = Enumerable.Range(0, 100)
                .Select(_ => new TestRequest())
                .ToList();

            foreach (var request in requests)
            {
                processor.ProcessAsync(request);
            }

            await Helpers.PollUntilAsync(() => requests.All(collection.IsHandled));
        });
    }
    
    private async Task ExecuteInScopeAsync(Func<IProcessor<TestRequest>, TestRequestCollection, Task> func)
    {
        await using var scope = _services.CreateAsyncScope();

        var processor = scope.ServiceProvider.GetRequiredService<IProcessor<TestRequest>>();
        var collection = scope.ServiceProvider.GetRequiredService<TestRequestCollection>();

        await func(processor, collection);
    }
}
