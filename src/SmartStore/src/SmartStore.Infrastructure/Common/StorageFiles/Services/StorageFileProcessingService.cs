using Azure.AI.Vision.ImageAnalysis;
using Microsoft.AspNetCore.Http;
using SmartStore.Application.Common.ContentAnalysis.Brokers;
using SmartStore.Application.Common.StorageFiles.Services;
using SmartStore.Domain.Models.Entities;

namespace SmartStore.Infrastructure.Common.StorageFiles.Services;

/// <summary>
/// Provides storage file processing service functionalities
/// </summary>
public class StorageFileProcessingService(IImageAnalysisApiClient imageAnalysisApiClient) : IStorageFileProcessingService
{
    public async ValueTask<List<StorageFile>> UploadAsync(IFormFileCollection files)
    {
        // Limit files
        var file = files.First();

        // Define features
        var visualAnalysisFeatures = VisualFeatures.Caption | VisualFeatures.DenseCaptions | VisualFeatures.Tags;

        // Define options
        var visualAnalysisOptions = new ImageAnalysisOptions
        {
            GenderNeutralCaption = true,
            Language = "en",
            SmartCropsAspectRatios = new[] { 0.9F, 1.33F }
        };
        
        // Analyse image
        var imageData = await BinaryData.FromStreamAsync(file.OpenReadStream());
        var result = await imageAnalysisApiClient.AnalyseAsync(imageData, visualAnalysisFeatures, visualAnalysisOptions);
        
        // Create storage file entity
        var storageFile = new StorageFile
        {
            Name = file.Name,
            Caption = result.Caption!.Text!,
            Tags = result.DenseCaptions.Values.Select(caption => caption.Text).Take(5).ToList(),
        };

        // Store in database
        
        return [storageFile];
    }
}