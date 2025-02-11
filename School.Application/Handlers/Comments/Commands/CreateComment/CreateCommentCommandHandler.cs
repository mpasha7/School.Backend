using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateCommentCommandHandler(
            ICourseRepository courseRepository,
            ICommentRepository commentRepository,
            IStudentRepository studentRepository)
        {
            this._courseRepository = courseRepository;
            this._commentRepository = commentRepository;
            this._studentRepository = studentRepository;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (!(await _studentRepository.IsStudentOfThisCourse(
                request.StudentGuid,
                request.CourseId,
                cancellationToken)))
                throw new NoAccessException(nameof(Course), request.CourseId);

            var comment = new Comment
            {
                StudentGuid = request.StudentGuid,
                StudentName = request.StudentName,

                CreatedAt = DateTime.Now,
                Text = request.Text,
                IsPublic = false,

                CourseId = request.CourseId
            };

            await _commentRepository.AddAsync(comment, cancellationToken);
            return comment.Id;
        }
    }
}
