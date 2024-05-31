namespace Speck.TemporalBatching.Internal;

internal record Envelope<TRequest>(Guid CorrelationId, TRequest Request);
