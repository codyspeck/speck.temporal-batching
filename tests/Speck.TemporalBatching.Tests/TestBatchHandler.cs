namespace Speck.TemporalBatching.Tests;

internal class TestBatchHandler(TestRequestCollection collection) : IBatchHandler<TestRequest>
{
    public Task HandleAsync(IReadOnlyCollection<TestRequest> requests)
    {
        foreach (var request in requests)
        {
            collection.Handle(request);
        }
        
        return Task.CompletedTask;
    }
}