using MediatR;

namespace School.Application.Handlers.Students.Commands.RemoveStudentFromCourse
{
    public class RemoveStudentFromCourseCommand : IRequest
    {
        public string? CoachGuid { get; set; }

        public string? StudentGuid { get; set; }
        public int CourseId { get; set; }
    }
}
