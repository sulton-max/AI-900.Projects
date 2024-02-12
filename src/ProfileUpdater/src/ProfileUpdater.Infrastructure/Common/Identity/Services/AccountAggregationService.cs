using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using ProfileUpdater.Application.Common.Identity.Services;
using ProfileUpdater.Application.Common.ImageFaceAnalysis.Brokers;
using ProfileUpdater.Domain.Models.Entities;

namespace ProfileUpdater.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides account aggregation service functionalities
/// </summary>
public class AccountAggregationService(IFaceRecognitionApiBroker faceRecognitionApiBroker) : IAccountAggregationService
{
    public async ValueTask<StorageFile> UploadProfilePhotoAsync(IFormFile file)
    {
        var errorsList = new List<string>();

        // Analyze file
        var detectedFaces = await faceRecognitionApiBroker.DetectFaces(file.OpenReadStream());

        // Validate faces count
        if (detectedFaces.Count is > 1 or 0)
            throw new ValidationException("Only one face is allowed in the image");

        // Validate quality
        if (detectedFaces[0].FaceAttributes.QualityForRecognition!.Value < QualityForRecognition.High)
            errorsList.Add("Face quality is too low");

        // Validate accessories
        if (detectedFaces[0].FaceAttributes.Accessories!.Any())
            errorsList.Add("Accessories are not allowed");

        // Validate exposure
        if (detectedFaces[0].FaceAttributes.Exposure!.Value < 0.4)
            errorsList.Add("Face is underexposed");
        else if (detectedFaces[0].FaceAttributes.Exposure!.Value > 0.7)
            errorsList.Add("Face is overexposed");

        // Validate glasses
        if (detectedFaces[0].FaceAttributes.Glasses is GlassesType.Sunglasses or GlassesType.SwimmingGoggles)
            errorsList.Add("Sunglasses and swimming goggles are not allowed");

        // Validate occlusion
        if (detectedFaces[0].FaceAttributes.Occlusion!.EyeOccluded || detectedFaces[0].FaceAttributes.Occlusion!.ForeheadOccluded ||
            detectedFaces[0].FaceAttributes.Occlusion!.MouthOccluded)
            errorsList.Add("Face occlusion is not allowed");

        // Validate head pose
        if (detectedFaces[0].FaceAttributes.HeadPose!.Pitch > 15 || detectedFaces[0].FaceAttributes.HeadPose!.Roll > 15 ||
            detectedFaces[0].FaceAttributes.HeadPose!.Yaw > 15)
            errorsList.Add("Head pose is not allowed");

        return errorsList.Count != 0
            ? throw new ValidationException(string.Join(", ", errorsList))
            : new StorageFile
            {
                Name = file.Name
            };
    }
}