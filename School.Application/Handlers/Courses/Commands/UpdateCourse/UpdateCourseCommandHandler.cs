using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository _repository;
        private readonly IFileService _fileService;

        public UpdateCourseCommandHandler(ICourseRepository repository, IFileService fileService)
        {
            this._repository = repository;
            this._fileService = fileService;
        }

        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _repository.GetByIdAsync(request.Id, cancellationToken, includeProperty: "Photo");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);

            course.Title = request.Title;
            course.Description = request.Description;
            course.ShortDescription = request.ShortDescription;
            course.PublicDescription = request.PublicDescription;
            course.BeginQuestionnaire = request.BeginQuestionnaire;
            course.EndQuestionnaire = request.EndQuestionnaire;

            if (request.FormFile != null)
            {
                await _fileService.DeleteFileAsync(course.Photo.Id, FileTypes.Photo, cancellationToken);
                await _fileService.SaveFileAsync(request.FormFile, FileTypes.Photo, FileOwners.Course, course.Id, cancellationToken);
            }

            await _repository.UpdateAsync(course, cancellationToken);
        }
    }
}
