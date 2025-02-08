using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Students.Commands.AddStudentToCourse
{
    public class AddStudentToCourseCommandHandler : IRequestHandler<AddStudentToCourseCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public AddStudentToCourseCommandHandler(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            this._studentRepository = studentRepository;
            this._courseRepository = courseRepository;
        }

        public async Task<int> Handle(AddStudentToCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);
            else if (await _studentRepository.IsStudentOfThisCourse(request.StudentGuid, request.CourseId, cancellationToken))
                throw new ActionAlreadyCompletedException("Student", request.StudentGuid, "already has", nameof(Course), request.CourseId);

            var studentOfCourse = new StudentOfCourse
            {
                StudentGuid = request.StudentGuid,
                CourseId = request.CourseId
            };

            await _studentRepository.AddAsync(studentOfCourse, cancellationToken);

            return studentOfCourse.Id;
        }
    }
}
