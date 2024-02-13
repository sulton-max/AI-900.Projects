using Microsoft.AspNetCore.Http;
using ResumeScanner.Application.Resumes.Models;

namespace ResumeScanner.Application.Resumes.Services;

/// <summary>
/// Defines resume processing service functionalities
/// </summary>
public interface IResumeProcessingService
{
    /// <summary>
    /// Uploads and analyzes resume
    /// </summary>
    /// <param name="resumeFile">Uploaded resume file</param>
    /// <returns>Id of resume</returns>
    ValueTask<ResumeAnalysisResult> UploadResumeAsync(IFormFile resumeFile);
}