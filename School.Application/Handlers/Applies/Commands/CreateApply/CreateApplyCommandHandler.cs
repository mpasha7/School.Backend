using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Applies.Commands.CreateApply
{
    public class CreateApplyCommandHandler : IRequestHandler<CreateApplyCommand, int>
    {
        private readonly IApplyRepository _applyRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateApplyCommandHandler(IApplyRepository applyRepository, ICourseRepository courseRepository)
        {
            this._applyRepository = applyRepository;
            this._courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateApplyCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken,
                includeCollection: "Students");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.Students
                .Any(s => s.StudentGuid == request.StudentGuid && s.CourseId == request.CourseId))
                throw new ActionAlreadyCompletedException("Student", request.StudentGuid, "already has", nameof(Course), request.CourseId);
            else if ((await _applyRepository.GetAllAsync(
                cancellationToken,
                filter: a => a.StudentGuid == request.StudentGuid && a.CourseId == request.CourseId)).Any())
                throw new ActionAlreadyCompletedException("Student", request.StudentGuid, "has already apply for", nameof(Course), request.CourseId);

            var apply = new Apply
            {
                StudentGuid = request.StudentGuid,
                StudentName = request.StudentName,
                IsAssepted = false,
                CourseId = request.CourseId
            };

            await _applyRepository.AddAsync(apply, cancellationToken);
            return apply.Id;
        }
    }
}
