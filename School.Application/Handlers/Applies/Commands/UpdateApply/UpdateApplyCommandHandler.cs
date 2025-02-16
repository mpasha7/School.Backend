using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Applies.Commands.UpdateApply
{
    public class UpdateApplyCommandHandler : IRequestHandler<UpdateApplyCommand>
    {
        private readonly IApplyRepository _applyRepository;
        private readonly IStudentRepository _studentRepository;

        public UpdateApplyCommandHandler(IApplyRepository applyRepository, IStudentRepository studentRepository)
        {
            this._applyRepository = applyRepository;
            this._studentRepository = studentRepository;
        }

        public async Task Handle(UpdateApplyCommand request, CancellationToken cancellationToken)
        {
            var apply = await _applyRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Course");

            if (apply == null)
                throw new NotFoundException(nameof(Apply), request.Id);
            else if (apply.Course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (apply.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId, nameof(Apply), request.Id);
            else if (apply.StudentGuid != request.StudentGuid)
                throw new NoAccessException(nameof(Apply), request.Id);
            else if (apply.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);
            else if (await _studentRepository.IsStudentOfThisCourse(request.StudentGuid, request.CourseId, cancellationToken))
                throw new ActionAlreadyCompletedException("Student", request.StudentGuid, "already has", nameof(Course), request.CourseId);

            var studentOfCourse = new StudentOfCourse
            {
                StudentGuid = request.StudentGuid,
                CourseId = request.CourseId
            };
            await _studentRepository.AddAsync(studentOfCourse, cancellationToken);

            await _applyRepository.DeleteAsync(apply, cancellationToken);
        }
    }
}
