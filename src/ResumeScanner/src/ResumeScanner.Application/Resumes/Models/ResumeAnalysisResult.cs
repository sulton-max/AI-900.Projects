using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ResumeScanner.Application.Resumes.Models;

/// <summary>
/// Represents resume analysis status
/// </summary>
/// <param name="Status"></param>
/// <param name="ResumeContent">Content of resume</param>
public record ResumeAnalysisResult(OperationStatusCodes Status, IEnumerable<string>? ResumeContent = default);