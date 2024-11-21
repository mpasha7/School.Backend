using MediatR;
using School.Application.Interfaces;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly ISchoolDbContext context;

        public CreateCourseCommandHandler(ISchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                CreatedDate = DateTime.Now,
                CoachGuid = request.CoachGuid,

                Title = request.Title,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                PublicDescription = request.PublicDescription,
                PhotoPath = request.PhotoPath,
                BeginQuestionnaire = request.BeginQuestionnaire,
                EndQuestionnaire = request.EndQuestionnaire
            };

            await context.Courses.AddAsync(course, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return course.Id;
        }
    }
}
