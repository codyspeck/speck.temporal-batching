using Sample.Application;
using Sample.Data;
using Speck.TemporalBatching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTemporalBatchProcessor<CreateAccountBatchBatchHandler, Account>(processor => processor
    .WithTimeout(TimeSpan.FromSeconds(5))
    .WithBatchSize(10));

var app = builder.Build();

app.MapGet("/accounts", (AccountRepository repository) => Results.Ok(repository.Accounts));

app.MapPost("/accounts", async (IProcessor<Account> processor, Account account) =>
{
    // The IProcessor implementation blocks on completion of the batch. 
    await processor.ProcessAsync(account);
    
    return Results.Ok();
});

app.MapPost("/accounts-async", (IProcessor<Account> processor, Account account) =>
{
    // Omitting "await" to provide an endpoint that can be hit multiple times by testing tools to demonstrate filling
    // up a batch of "Account" requests.
    processor.ProcessAsync(account);
    
    return Results.Ok();
});
    
app.Run();
