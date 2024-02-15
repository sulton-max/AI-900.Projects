namespace ResumeScanner.Application.Common.Caching.Brokers;

/// <summary>
/// Defines cache broker
/// </summary>
public interface ICacheBroker
{
    /// <summary>
    /// Gets a value from the cache if it exists, otherwise sets it
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="valueFactory">Factory to get new value</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="T">Value type</typeparam>
    /// <returns>Value from cache if found, otherwise generated new value</returns>
    ValueTask<T?> GetOrAddAsync<T>(string key, Func<ValueTask<T>> valueFactory, CancellationToken cancellationToken = default);
}