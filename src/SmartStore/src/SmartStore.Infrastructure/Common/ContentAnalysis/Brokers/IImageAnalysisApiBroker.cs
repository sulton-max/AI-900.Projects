using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Extensions.Options;
using SmartStore.Application.Common.ContentAnalysis.Brokers;
using SmartStore.Infrastructure.Common.ContentAnalysis.Settings;

namespace SmartStore.Infrastructure.Common.ContentAnalysis.Brokers;

/// <summary>
/// Provides image analysis broker functionality
/// </summary>
public class ImageAnalysisApiClient(IOptions<ImageAnalysisApiSettings> imageAnalysisApiSettings, ImageAnalysisClient imageAnalysisClient)
    : IImageAnalysisApiClient
{
    private readonly ImageAnalysisApiSettings _imageAnalysisApiSettings = imageAnalysisApiSettings.Value;

    public async ValueTask<ImageAnalysisResult> AnalyseAsync(BinaryData imageData, VisualFeatures visualFeatures, ImageAnalysisOptions options)
    {
        var client = new ImageAnalysisClient(
            new Uri(_imageAnalysisApiSettings.BaseAddress),
            new AzureKeyCredential(_imageAnalysisApiSettings.ApiKey)
        );

        var result = await imageAnalysisClient.AnalyzeAsync(imageData, visualFeatures, options);
        return result.HasValue ? result.Value : throw new InvalidOperationException("No analysis result returned.");
    }
}