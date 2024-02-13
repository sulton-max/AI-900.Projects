using System.Security.Cryptography;
using System.Text;
using ResumeScanner.Application.Common.StorageFiles.Brokers;

namespace ResumeScanner.Infrastructure.Common.StorageFiles.Brokers;

/// <summary>
/// Provides file checksum provider functionality
/// </summary>
public class FileChecksumProvider : IFileChecksumProvider
{
    public string ComputeChecksum(Stream fileStream)
    {
        using var sha256 = SHA256.Create();

        // Compute the hash of the image file
        var stringBuilder = new StringBuilder();
        var hashBytes = sha256.ComputeHash(fileStream).ToList();
        hashBytes.ForEach(hashByte => stringBuilder.Append(hashByte.ToString("X2")));

        // Reset the stream position
        fileStream.Position = 0;

        return stringBuilder.ToString();
    }
}