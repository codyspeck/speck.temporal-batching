using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Speck.TemporalBatching.Internal;

namespace Speck.TemporalBatching;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTemporalBatchProcessor<THandler, TRequest>(
        this IServiceCollection services,
        Action<PipelineConfiguration> configurator)
        where THandler : class, IBatchHandler<TRequest>
    {
        var configuration = new PipelineConfiguration();

        configurator(configuration);
        
        services.TryAddSingleton<IOrchestrator, Orchestrator>();
        services.AddSingleton(provider => PipelineBuilder.Build<TRequest>(provider, configuration));
        services.AddScoped<IBatchHandler<TRequest>, THandler>();
        services.AddScoped<IProcessor<TRequest>, Processor<TRequest>>();

        return services;
    }
}
