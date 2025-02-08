using Microsoft.AspNetCore.Http;
using School.Domain;

namespace School.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<int> SaveFileAsync(
            IFormFile formFile,
            FileTypes fileType,
            FileOwners fileOwner,
            int ownerId,
            CancellationToken cancellationToken);
        Task DeleteFileAsync(
            int id,
            FileTypes fileType,
            CancellationToken cancellationToken);
    }
}
