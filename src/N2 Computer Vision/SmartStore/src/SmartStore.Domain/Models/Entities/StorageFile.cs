namespace SmartStore.Domain.Models.Entities;

/// <summary>
/// Represents storage file
/// </summary>
public class StorageFile
{
    /// <summary>
    /// Gets or sets the file name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets file tags
    /// </summary>
    public IList<string> Tags { get; set; } = default!;

    /// <summary>
    /// Gets or sets caption
    /// </summary>
    public string Caption { get; set; } = default!;
}