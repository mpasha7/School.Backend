using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;

namespace School.Application.Handlers.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonNumbersService _numbersService;

        public CreateLessonCommandHandler(
            ILessonRepository lessonRepository,
            ICourseRepository courseRepository,
            ILessonNumbersService numbersService)
        {
            this._lessonRepository = lessonRepository;
            this._courseRepository = courseRepository;
            this._numbersService = numbersService;
        }

        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            int maxLessonNumber = await _lessonRepository.GetMaxLessonNumber(course.Id, cancellationToken);
            if (request.Number <= maxLessonNumber)
                await _numbersService.ShiftNumbersIfCreateLesson(request.Number, course.Id, cancellationToken);

            var lesson = new Lesson
            {
                Number = request.Number,
                Title = request.Title,
                Description = request.Description,
                VideoLink = request.VideoLink,

                CourseId = request.CourseId
            };

            await _lessonRepository.AddAsync(lesson, cancellationToken);
            return lesson.Id;
        }
    }
}
