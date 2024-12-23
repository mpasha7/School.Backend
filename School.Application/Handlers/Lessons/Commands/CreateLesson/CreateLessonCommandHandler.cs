using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
    {        
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateLessonCommandHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository)
        {
            this._lessonRepository = lessonRepository;
            this._courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var lesson = new Lesson
            {
                CourseId = request.CourseId,

                Number = request.Number,
                Title = request.Title,
                Description = request.Description,
                VideoLink = request.VideoLink
            };

            await _lessonRepository.AddAsync(lesson, cancellationToken);
            return lesson.Id;
        }
    }
}
