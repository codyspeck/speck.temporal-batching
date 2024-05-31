namespace Speck.TemporalBatching;

/// <summary>
/// A user-defined handler for handling a batch of objects of the specified type.
/// </summary>
public interface IBatchHandler<in TRequest>
{
    public Task HandleAsync(IReadOnlyCollection<TRequest> requests);
}
