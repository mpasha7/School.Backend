using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Students.Commands.RemoveStudentFromCourse
{
    public class RemoveStudentFromCourseCommandHandler : IRequestHandler<RemoveStudentFromCourseCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public RemoveStudentFromCourseCommandHandler(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            this._studentRepository = studentRepository;
            this._courseRepository = courseRepository;
        }

        public async Task Handle(RemoveStudentFromCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var student = (await _studentRepository.GetAllAsync(
                cancellationToken,
                filter: s => s.StudentGuid == request.StudentGuid && s.CourseId == request.CourseId))
                .SingleOrDefault();
            if (student == null)
                throw new ActionAlreadyCompletedException("Student", request.StudentGuid, "not has", nameof(Course), request.CourseId);

            await _studentRepository.DeleteAsync(student, cancellationToken);
        }
    }
}
