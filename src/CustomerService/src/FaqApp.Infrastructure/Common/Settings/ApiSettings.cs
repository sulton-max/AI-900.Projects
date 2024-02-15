namespace FaqApp.Infrastructure.Common.Settings;

/// <summary>
/// Represents API settings
/// </summary>
public record ApiSettings
{
    /// <summary>
    /// Gets API base address
    /// </summary>
    public string BaseAddress { get; init; } = default!;
}