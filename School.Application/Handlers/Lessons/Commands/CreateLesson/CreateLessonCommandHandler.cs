using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
using School.Domain;

namespace School.Application.Handlers.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
    {
        private readonly ISchoolDbContext context;

        public CreateLessonCommandHandler(ISchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.CourseId);

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

            await context.Lessons.AddAsync(lesson, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return lesson.Id;
        }
    }
}
