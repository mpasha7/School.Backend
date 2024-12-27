using Microsoft.AspNetCore.Http;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<int> SaveFileAsync(
            IFormFile formFile,
            FileTypes fileType,
            FileOwners fileOwners,
            int courseId,
            CancellationToken cancellationToken);
        Task DeleteFileAsync(
            int id,
            FileTypes fileType,
            CancellationToken cancellationToken);
    }
}
