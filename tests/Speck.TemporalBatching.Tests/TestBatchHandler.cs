namespace Speck.TemporalBatching.Tests;

internal class TestBatchHandler(TestRequestCollection collection) : IBatchHandler<TestRequest>
{
    public Task HandleAsync(IReadOnlyCollection<TestRequest> requests)
    {
        collection.Handle(requests);
        
        return Task.CompletedTask;
    }
}