using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ResumeScanner.Application.Common.DocumentProcessing.Brokers;

/// <summary>
/// Defines the document scanner API broker
/// </summary>
public interface IDocumentScannerApiBroker
{
    /// <summary>
    /// Uploads document to analyze
    /// </summary>
    /// <param name="documentStream">Document in stream</param>
    /// <returns>Queued operation id</returns>
    ValueTask<Guid> UploadForAnalyzeAsync(Stream documentStream);

    /// <summary>
    /// Gets operation result
    /// </summary>
    /// <param name="operationId">Queued operation Id</param>
    /// <returns>Operation status for current document</returns>
    ValueTask<ReadOperationResult> GetOperationResult(Guid operationId);
}