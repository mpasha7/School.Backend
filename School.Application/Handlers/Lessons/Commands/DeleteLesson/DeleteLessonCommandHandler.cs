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

namespace School.Application.Handlers.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonNumbersService _numbersService;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository, ILessonNumbersService numbersService)
        {
            this._lessonRepository = lessonRepository;
            this._numbersService = numbersService;
        }

        public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Course");

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.Id);
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Lesson), request.Id);

            await _numbersService.ShiftNumbersIfDeleteLesson(lesson.Number, lesson.CourseId, cancellationToken);

            await _lessonRepository.DeleteAsync(lesson, cancellationToken);
        }
    }
}
