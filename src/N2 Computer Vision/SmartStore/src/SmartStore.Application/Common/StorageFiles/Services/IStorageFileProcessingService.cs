using Microsoft.AspNetCore.Http;
using SmartStore.Domain.Models.Entities;

namespace SmartStore.Application.Common.StorageFiles.Services;

/// <summary>
/// Defines storage file processing service functionalities
/// </summary>
public interface IStorageFileProcessingService
{
    /// <summary>
    /// Uploads file to storage and returns file as storage file
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    ValueTask<List<StorageFile>> UploadAsync(IFormFileCollection files);
}