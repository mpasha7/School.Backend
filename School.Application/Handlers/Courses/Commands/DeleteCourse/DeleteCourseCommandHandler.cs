﻿using MediatR;
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

namespace School.Application.Handlers.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IFileService _fileService;

        public DeleteCourseCommandHandler(
            ICourseRepository courseRepository,
            IFileService fileService)
        {
            this._courseRepository = courseRepository;
            this._fileService = fileService;
        }

        public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken, includeReference: "Photo");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);

            await _fileService.DeleteFileAsync(course.Photo.Id, FileTypes.Photo, cancellationToken);

            await _courseRepository.DeleteAsync(course, cancellationToken);
        }
    }
}
