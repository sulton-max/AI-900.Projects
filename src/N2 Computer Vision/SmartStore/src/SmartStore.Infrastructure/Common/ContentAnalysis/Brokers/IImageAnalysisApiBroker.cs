using Azure.AI.Vision.ImageAnalysis;
using SmartStore.Application.Common.ContentAnalysis.Brokers;

namespace SmartStore.Infrastructure.Common.ContentAnalysis.Brokers;

/// <summary>
/// Provides image analysis broker functionality
/// </summary>
public class ImageAnalysisApiClient(ImageAnalysisClient imageAnalysisClient)
    : IImageAnalysisApiClient
{
    public async ValueTask<ImageAnalysisResult> AnalyseAsync(BinaryData imageData, VisualFeatures visualFeatures, ImageAnalysisOptions options)
    {
        var result = await imageAnalysisClient.AnalyzeAsync(imageData, visualFeatures, options);
        return result.HasValue ? result.Value : throw new InvalidOperationException("No analysis result returned.");
    }
}