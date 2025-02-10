using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Assessments.Commands.CreateAssessment
{
    public class CreateAssessmentCommandHandler : IRequestHandler<CreateAssessmentCommand, int>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateAssessmentCommandHandler(
            ICourseRepository courseRepository,
            IAssessmentRepository assessmentRepository,
            IStudentRepository studentRepository)
        {
            this._courseRepository = courseRepository;
            this._assessmentRepository = assessmentRepository;
            this._studentRepository = studentRepository;
        }

        public async Task<int> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);
            else if (!(await _studentRepository.IsStudentOfThisCourse(
                request.StudentGuid,
                request.CourseId,
                cancellationToken)))
                throw new NoAccessException(nameof(StudentOfCourse), 0);

            var assessment = new Assessment
            {
                StudentGuid = request.StudentGuid,
                CreatedAt = DateTime.Now,
                Text = request.Text,
                CourseId = request.CourseId,
            };

            await _assessmentRepository.AddAsync(assessment, cancellationToken);

            return assessment.Id;
        }
    }
}
