namespace Speck.TemporalBatching.Tests;

internal class TestRequestCollection
{
    private readonly List<TestRequest> _handledRequests = [];

    public void Handle(TestRequest request) => _handledRequests.Add(request);

    public bool IsHandled(TestRequest request) => _handledRequests.Any(handled => handled == request);
}
