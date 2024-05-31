namespace Speck.TemporalBatching;

public class PipelineConfiguration
{
    internal int BatchSize { get; private set; } = 10;

    internal int MaxDegreeOfParallelism { get; private set; } = 1;

    internal TimeSpan Timeout { get; private set; } = TimeSpan.FromMilliseconds(5);

    /// <summary>
    /// Configure a batch size. This represents the maximum size of a batch sent to this pipeline's handler. If the
    /// pipeline sits idle for the configured timeout window without receiving messages, a smaller batch may be sent.
    /// The default is 10.
    /// </summary>f
    public PipelineConfiguration WithBatchSize(int batchSize)
    {
        BatchSize = batchSize;
        return this;
    }

    /// <summary>
    /// Configure a max degree of parallelism. This represents the maximum number of concurrent batches that can be
    /// processed at any given time for this pipeline; or in other words, the maximum number of batch handlers executing
    /// concurrently for this message type.
    /// </summary>
    public PipelineConfiguration WithMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        MaxDegreeOfParallelism = maxDegreeOfParallelism;
        return this;
    }
    
    /// <summary>
    /// Configure a timeout value. This represents the amount of time the application will sit idle before
    /// flushing messages through its internal buffer if a batch of the configured size is not reached. The default is
    /// five milliseconds.
    /// </summary>
    public PipelineConfiguration WithTimeout(TimeSpan timeout)
    {
        Timeout = timeout;
        return this;
    }
}
