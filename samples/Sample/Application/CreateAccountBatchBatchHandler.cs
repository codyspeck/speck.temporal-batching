using Sample.Data;
using Speck.TemporalBatching;

namespace Sample.Application;

public class CreateAccountBatchBatchHandler(AccountRepository repository) : IBatchHandler<Account>
{
    public Task HandleAsync(IReadOnlyCollection<Account> requests)
    {
        Console.WriteLine($"Handling batch of {requests.Count} accounts.");
        
        repository.AddBatch(requests);
        
        return Task.CompletedTask;
    }
}
