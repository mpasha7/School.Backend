using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Course");

            if (comment == null)
                throw new NotFoundException(nameof(Comment), request.Id);
            else if (comment.Course == null)
                throw new NotFoundException(nameof(Course), comment.CourseId);
            else if (comment.Course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), comment.CourseId);

            comment.IsPublic = !comment.IsPublic;

            await _commentRepository.UpdateAsync(comment, cancellationToken);
        }
    }
}
