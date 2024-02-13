namespace ResumeScanner.Application.Common.StorageFiles.Brokers;

/// <summary>
/// Defines file checksum provider functionality
/// </summary>
public interface IFileChecksumProvider
{
    /// <summary>
    /// Computes the checksum of the file
    /// </summary>
    /// <param name="fileStream">Filestream to compute checksum</param>
    /// <returns>Checksum of a file</returns>
    string ComputeChecksum(Stream fileStream);
}