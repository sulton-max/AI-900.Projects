using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using ResumeScanner.Application.Common.Caching.Brokers;
using ResumeScanner.Application.Common.DocumentProcessing.Brokers;
using ResumeScanner.Application.Common.StorageFiles.Brokers;
using ResumeScanner.Application.Resumes.Models;
using ResumeScanner.Application.Resumes.Services;

namespace ResumeScanner.Infrastructure.Resumes.Services;

/// <summary>
/// Provides resume processing service functionalities
/// </summary>
public class ResumeProcessingService(
    IFileChecksumProvider fileChecksumProvider,
    IDocumentScannerApiBroker documentScannerApiBroker,
    ICacheBroker cacheBroker
) : IResumeProcessingService
{
    public async ValueTask<ResumeAnalysisResult> UploadResumeAsync(IFormFile resumeFile)
    {
        // Compute file checksum
        await using var fileStream = resumeFile.OpenReadStream();
        var checksum = fileChecksumProvider.ComputeChecksum(fileStream);

        // Get operation Id
        var operationId = await cacheBroker.GetOrAddAsync(checksum, () => documentScannerApiBroker.UploadForAnalyzeAsync(fileStream));

        // Get operation result
        var operationResult = await documentScannerApiBroker.GetOperationResult(operationId);

        // Read result
        if (operationResult.Status is not OperationStatusCodes.Succeeded)
            return new ResumeAnalysisResult(operationResult.Status);
        
        var resumeContent = operationResult.AnalyzeResult.ReadResults.
            SelectMany(readResult => readResult.Lines)
            .Select(line => line.Text);

        return new ResumeAnalysisResult(operationResult.Status, resumeContent);
    }
}