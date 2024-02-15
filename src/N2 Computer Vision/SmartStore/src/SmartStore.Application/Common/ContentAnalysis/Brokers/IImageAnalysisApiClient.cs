using Azure.AI.Vision.ImageAnalysis;

namespace SmartStore.Application.Common.ContentAnalysis.Brokers;

/// <summary>
/// Defines image analysis broker functionality
/// </summary>
public interface IImageAnalysisApiClient
{
    /// <summary>
    /// Sends request to Cognitive Services to analyse image
    /// </summary>
    /// <param name="imageData">Image to analyze</param>
    /// <param name="visualFeatures">Visual features</param>
    /// <param name="options">Analysis options</param>
    /// <returns></returns>
    ValueTask<ImageAnalysisResult> AnalyseAsync(BinaryData imageData, VisualFeatures visualFeatures, ImageAnalysisOptions options);
}