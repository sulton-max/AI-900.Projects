using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.DependencyInjection;
using ResumeScanner.Application.Common.DocumentProcessing.Brokers;

namespace ResumeScanner.Infrastructure.Common.DocumentProcessing.Brokers;

/// <summary>
/// Provides the document scanner API broker
/// </summary>
public class DocumentScannerApiBroker([FromKeyedServices("DocumentAnalysis")] IComputerVisionClient computerVisionClient) : IDocumentScannerApiBroker
{
    public async ValueTask<Guid> UploadForAnalyzeAsync(Stream documentStream)
    {
        var operationLocation = await computerVisionClient.ReadInStreamAsync(documentStream);
        return Guid.Parse(new Uri(operationLocation.OperationLocation).Segments.Last());
    }

    public async ValueTask<ReadOperationResult> GetOperationResult(Guid operationId)
    {
        return await computerVisionClient.GetReadResultAsync(operationId);
    }
}