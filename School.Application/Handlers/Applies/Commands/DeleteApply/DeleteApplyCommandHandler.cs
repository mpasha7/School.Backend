using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Applies.Commands.DeleteApply
{
    public class DeleteApplyCommandHandler : IRequestHandler<DeleteApplyCommand>
    {
        private readonly IApplyRepository _applyRepository;

        public DeleteApplyCommandHandler(IApplyRepository applyRepository)
        {
            this._applyRepository = applyRepository;
        }

        public async Task Handle(DeleteApplyCommand request, CancellationToken cancellationToken)
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
            else if (apply.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);

            await _applyRepository.DeleteAsync(apply, cancellationToken);
        }
    }
}
