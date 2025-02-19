﻿using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;

namespace School.WebApi.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IFileRepository _fileRepository;
        private readonly Dictionary<FileTypes, string> fileTypes 
            = Enum.GetValues(typeof(FileTypes))
                .Cast<FileTypes>()
                .ToDictionary(t => t, t => t.ToString());

        public FileService(IWebHostEnvironment environment, IFileRepository fileRepository)
        {
            this._environment = environment;
            this._fileRepository = fileRepository;
        }

        public async Task<int> SaveFileAsync(
            IFormFile formFile,
            FileTypes fileType,
            FileOwners fileOwner,
            int ownerId,
            CancellationToken cancellationToken)
        {
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));

            var rootPath = _environment.WebRootPath;
            var uploadPath = Path.Combine(rootPath, "uploads", fileTypes[fileType]);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = formFile.FileName;
            var fileNameExt = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{fileName}";
            var fileSize = formFile.Length;

            var fileNameWithPath = Path.Combine(uploadPath, uniqueFileName);
            using (var fs = new FileStream(fileNameWithPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fs);
            }

            FileObject file = new FileObject
            {
                CreatedAt = DateTime.Now,

                UniqueFileName = uniqueFileName,
                FileName = fileName,
                FileNameExt = fileNameExt,
                FileSize = fileSize,
                FileType = fileType,
                FileOwner = fileOwner
            };

            switch (fileOwner)
            {
                case FileOwners.Course:
                    file.CourseId = ownerId;
                    break;
                case FileOwners.Lesson:
                    file.LessonId = ownerId;
                    break;
                case FileOwners.Report:
                    file.ReportId = ownerId;
                    break;
                default:
                    throw new ArgumentNullException(nameof(fileOwner));
            }

            await _fileRepository.AddAsync(file, cancellationToken);
            return file.Id;
        }

        public async Task DeleteFileAsync(int id, FileTypes fileType, CancellationToken cancellationToken)
        {
            FileObject? file = await _fileRepository.GetByIdAsync(id, cancellationToken);

            if (file == null)
                throw new NotFoundException(nameof(File), id);

            var rootPath = _environment.WebRootPath;
            var uploadPath = Path.Combine(rootPath, "uploads", fileTypes[fileType]);

            var fileNameWithPath = Path.Combine(uploadPath, file.UniqueFileName);
            if (File.Exists(fileNameWithPath))
                File.Delete(fileNameWithPath);

            await _fileRepository.DeleteAsync(file, cancellationToken);
        }
    }
}
