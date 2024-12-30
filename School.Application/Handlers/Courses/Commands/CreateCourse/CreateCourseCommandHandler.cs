using MediatR;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Application.Interfaces.Services;
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
        private readonly IFileService _fileService;

        public CreateCourseCommandHandler(ICourseRepository repository, IFileService fileService)
        {
            this._repository = repository;
            this._fileService = fileService;
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
                BeginQuestionnaire = request.BeginQuestionnaire,
                EndQuestionnaire = request.EndQuestionnaire
            };

            await _repository.AddAsync(course, cancellationToken);

            if (request.FormFile != null)
                await _fileService.SaveFileAsync(request.FormFile, FileTypes.Photo, FileOwners.Course, course.Id, cancellationToken);

            return course.Id;
        }
    }
}
