namespace Speck.TemporalBatching.Tests;

internal class TestRequestCollection
{
    private readonly List<List<TestRequest>> _handledRequestBatches = [];

    public IReadOnlyCollection<IReadOnlyCollection<TestRequest>> HandledRequestBatches => _handledRequestBatches;
    
    public void Handle(IEnumerable<TestRequest> batch) => _handledRequestBatches.Add(batch.ToList());
}
