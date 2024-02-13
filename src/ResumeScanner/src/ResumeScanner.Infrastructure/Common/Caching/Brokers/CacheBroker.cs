using Microsoft.Extensions.Caching.Memory;
using ResumeScanner.Application.Common.Caching.Brokers;

namespace ResumeScanner.Infrastructure.Common.Caching.Brokers;

/// <summary>
/// Provides cache broker
/// </summary>
public class CacheBroker(IMemoryCache memoryCache) : ICacheBroker
{
    public async ValueTask<T?> GetOrAddAsync<T>(string key, Func<ValueTask<T>> valueFactory, CancellationToken cancellationToken = default)
    {
        return await memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                return await valueFactory();
            }
        );
    }
}