namespace Speck.TemporalBatching;

public interface IProcessor<in TRequest>
{
    public Task ProcessAsync(TRequest request);
}
