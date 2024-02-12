using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace ProfileUpdater.Application.Common.ImageFaceAnalysis.Brokers;

/// <summary>
/// Defines face recognition API broker functionalities
/// </summary>
public interface IFaceRecognitionApiBroker
{
    /// <summary>
    /// Detects faces in the uploaded file
    /// </summary>
    /// <param name="fileStream">File to analyzes</param>
    /// <returns>List of detected faces</returns>
    ValueTask<IReadOnlyList<DetectedFace>> DetectFaces(Stream fileStream);
}