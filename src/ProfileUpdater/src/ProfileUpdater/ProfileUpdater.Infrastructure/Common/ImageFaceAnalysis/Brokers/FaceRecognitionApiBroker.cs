using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfileUpdater.Application.Common.ImageFaceAnalysis.Brokers;
using ProfileUpdater.Infrastructure.Common.ImageFaceAnalysis.Settings;

namespace ProfileUpdater.Infrastructure.Common.ImageFaceAnalysis.Brokers;

/// <summary>
/// Provides face recognition API broker functionalities
/// </summary>
public class FaceRecognitionApiBroker(
    IOptions<FaceRecognitionApiSettings> faceRecognitionApiSettings,
    [FromKeyedServices("ProfilePhotoAnalysis")] IFaceClient faceClient
) : IFaceRecognitionApiBroker
{
    private readonly FaceRecognitionApiSettings _faceRecognitionApiSettings = faceRecognitionApiSettings.Value;

    public async ValueTask<IReadOnlyList<DetectedFace>> DetectFaces(Stream fileStream)
    {
        var detectedFaces = await faceClient.Face.DetectWithStreamAsync(
            fileStream,
            recognitionModel: _faceRecognitionApiSettings.RecognitionModel,
            detectionModel: _faceRecognitionApiSettings.DetectionModel,
            returnFaceAttributes: new List<FaceAttributeType>
            {
                FaceAttributeType.QualityForRecognition,
                FaceAttributeType.Accessories,
                FaceAttributeType.Exposure,
                FaceAttributeType.Glasses,
                FaceAttributeType.Occlusion,
                FaceAttributeType.HeadPose
            }
        );

        return detectedFaces.AsReadOnly();
    }
}