namespace Speck.TemporalBatching.Tests;

internal static class Helpers
{
    public static async Task PollUntilAsync(Func<bool> func)
    {
        var source = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        while (!source.IsCancellationRequested)
        {
            if (func())
            {
                return;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100), source.Token);
        }
        
        throw new TimeoutException("Polling timed out.");
    }    
}
