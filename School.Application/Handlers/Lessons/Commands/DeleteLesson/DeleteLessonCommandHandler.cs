using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Interfaces;
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
        private readonly ISchoolDbContext context;

        public DeleteLessonCommandHandler(ISchoolDbContext context)
        {
            this.context = context;
        }

        public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(les => les.Course).FirstOrDefaultAsync(les => les.Id == request.Id, cancellationToken);

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);
            else if (lesson.Course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (lesson.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Lesson), request.Id);
            else if (lesson.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Lesson), request.Id);

            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
