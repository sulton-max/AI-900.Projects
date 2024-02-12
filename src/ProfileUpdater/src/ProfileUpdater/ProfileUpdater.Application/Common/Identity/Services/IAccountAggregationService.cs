using Microsoft.AspNetCore.Http;
using ProfileUpdater.Domain.Models.Entities;

namespace ProfileUpdater.Application.Common.Identity.Services;

/// <summary>
/// Defines account aggregation service functionalities
/// </summary>
public interface IAccountAggregationService
{
    /// <summary>
    /// Validates and registers user profile photo
    /// </summary>
    /// <param name="file">Uploaded file</param>
    /// <returns>Stored file</returns>
    ValueTask<StorageFile> UploadProfilePhotoAsync(IFormFile file);
}