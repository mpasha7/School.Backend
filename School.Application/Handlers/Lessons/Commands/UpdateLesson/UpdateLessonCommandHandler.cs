using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
using School.Domain;

namespace School.Application.Handlers.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonNumbersService _numbersService;

        public UpdateLessonCommandHandler(ILessonRepository lessonRepository, ILessonNumbersService numbersService)
        {
            this._lessonRepository = lessonRepository;
            this._numbersService = numbersService;
        }

        public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
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

            if (lesson.Number != request.Number)
                await _numbersService.ShiftNumbersIfUpdateLesson(
                    lesson.Number,
                    request.Number,
                    lesson.Id,
                    lesson.CourseId,
                    cancellationToken);

            lesson.Number = request.Number;
            lesson.Title = request.Title;
            lesson.Description = request.Description;
            lesson.VideoLink = request.VideoLink;

            await _lessonRepository.UpdateAsync(lesson, cancellationToken);
        }
    }
}
