using MediatR;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
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
        private readonly ICourseRepository _repository;

        public CreateCourseCommandHandler(ICourseRepository repository)
        {
            this._repository = repository;
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

            await _repository.AddAsync(course, cancellationToken);
            return course.Id;
        }
    }
}
